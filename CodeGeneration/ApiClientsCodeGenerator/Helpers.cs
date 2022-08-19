using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Attributes.ApiClientsCodeGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	internal static class Helpers
	{
		public static bool IsApiController(this Type type)
		{
			return typeof(ControllerBase).IsAssignableFrom(type) &&
				type.IsDefined(typeof(ApiControllerAttribute)) &&
				!type.IsDefined(typeof(DisableCodeGenerationAttribute)) &&
				type.GetMethods().Any(method => method.IsControllerAction());
		}

		public static string GetDisplayName<T>(this T value) where T : Enum
		{
			return value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)
				.Cast<DisplayAttribute>().FirstOrDefault()?.GetName() ?? value.ToString();
		}


		public static string GetControllerName(this Type type)
		{
			if (!type.IsApiController())
			{
				throw new ArgumentException(nameof(type));
			}
			return type.Name.EndsWith("Controller") ? type.Name[..^"Controller".Length] : type.Name;
		}

		public static string GetActionUrl(this MethodInfo method)
		{
			if (!method.DeclaringType.IsApiController() || !method.IsControllerAction())
			{
				throw new ArgumentException("Method isn't an action", nameof(method));
			}
			var actionTemplate = method.GetCustomAttributes().OfType<IRouteTemplateProvider>().OrderBy(item => item.Order ?? 0).FirstOrDefault()?.Template;
			var controllerTemplate = method.DeclaringType?.GetCustomAttributes().OfType<IRouteTemplateProvider>().OrderBy(item => item.Order ?? 0).FirstOrDefault()?.Template;
			if (string.IsNullOrEmpty(actionTemplate))
			{
				actionTemplate = controllerTemplate;
			} 
			else
			{
				if (!actionTemplate.StartsWith("/"))
				{
					if (controllerTemplate != null)
					{
						actionTemplate = controllerTemplate.TrimEnd('/') + '/' + actionTemplate.TrimStart('/');
					}
				}
			}
			actionTemplate ??= "[controller]/[action]";
			var url = actionTemplate.Replace("[controller]", GetControllerName(method.DeclaringType));
			url = url.Replace("[action]",
				method.GetCustomAttribute<ActionNameAttribute>()?.Name ?? method.Name);
			url = new Regex("v{.*:apiVersion}/").Replace(url, "");
			return url.TrimStart('/');
		}

		public static IList<MethodInfo> GetControllerActions(this Type type)
		{
			return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Where(method => 
				method.IsControllerAction()).ToList();
		}

		public static bool IsControllerAction(this MethodInfo method)
		{
			return method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && 
				!method.IsDefined(typeof(DisableCodeGenerationAttribute));
		}

		public static Type GetActionResponseType(this MethodInfo method)
		{
			var returnType = method.ReturnType;
			if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
			{
				returnType = returnType.GetGenericArguments().FirstOrDefault();
			}
			if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ActionResult<>))
			{
				return returnType.GetGenericArguments().FirstOrDefault();
			}
			return returnType;
		}

		public static bool IsAuthorizationRequired(this MethodInfo method)
		{
			return !method.IsDefined(typeof(AllowAnonymousAttribute)) && (method.IsDefined(typeof(AuthorizeAttribute)) || method.DeclaringType.IsDefined(typeof(AuthorizeAttribute)));
		}

		public static string GetActionHttpMethod(this MethodInfo method)
		{
			return method.GetCustomAttributes().OfType<IActionHttpMethodProvider>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() ?? "POST";
		}

		public static ParameterInfo GetActionBodyParameter(this MethodInfo method)
		{
			if (method.GetActionHttpMethod() == HttpMethods.Get || method.GetActionHttpMethod() == HttpMethods.Delete)
			{
				return null;
			}
			var result = method.GetParameters().FirstOrDefault(item => item.IsDefined(typeof(FromBodyAttribute)));
			if (result != null)
			{
				return result;
			}
			return method.GetParameters().FirstOrDefault(item => !item.IsDefined(typeof(FromQueryAttribute)) && !item.ParameterType.IsPrimitive && !item.ParameterType.IsEnum);
		}

		public static List<ActionQueryParameter> GetActionQueryParameters(this MethodInfo method)
		{
			var result = new SortedDictionary<string, ActionQueryParameter>();
			var bodyParameter = method.GetActionBodyParameter();
			foreach (var parameter in method.GetParameters())
			{
				if (parameter == bodyParameter)
				{
					continue;
				}
				var parameterType = parameter.ParameterType;
				var canBeNull = parameter.CanBeNull() && (!parameterType.IsValueType || parameterType.IsNullable());
				if (parameterType.IsNullable())
				{
					parameterType = Nullable.GetUnderlyingType(parameterType);
				}
				if (parameterType.IsSystem() || parameterType.IsEnum || parameterType.IsList())
				{
					result[parameter.Name] = new ActionQueryParameter(parameter.Name, 
						new[] { new ActionQueryParameter.PathItem(parameter.Name, canBeNull) },
						parameterType);
				}
				else
				{
					AddQueryParameters(parameter.ParameterType, "", new List<ActionQueryParameter.PathItem> { new ActionQueryParameter.PathItem(parameter.Name, canBeNull) }, result);
				}
			}
			return result.ToList().Select(item => item.Value).OrderBy(item => item.Name).ToList();
		}

		private static void AddQueryParameters(Type type, string name, List<ActionQueryParameter.PathItem> path, IDictionary<string, ActionQueryParameter> result)
		{
			foreach (var property in type.GetDataMembers())
			{
				var propertyName = name == "" ? property.Name : name + "." + property.Name;
				var propertyType = property.PropertyType;
				var canBeNull = property.CanBeNull() && (!propertyType.IsValueType || propertyType.IsNullable());
				if (propertyType.IsNullable())
				{
					propertyType = Nullable.GetUnderlyingType(propertyType);
				}
				path.Add(new ActionQueryParameter.PathItem(property.Name.ToCamelCase(), canBeNull));
				if (propertyType.IsSystem() || propertyType.IsEnum || propertyType.IsList())
				{
					result[propertyName] = new ActionQueryParameter(propertyName, path.ToList(), propertyType);
				} 
				else
				{
					AddQueryParameters(propertyType, propertyName, path, result);
				}
				path.RemoveAt(path.Count - 1);
			}
		}

		public static IEnumerable<PropertyInfo> GetDataMembers(this Type type)
		{
			return type.GetProperties().Where(item => !item.IsDefined(typeof(JsonIgnoreAttribute)) && !item.IsDefined(typeof(DisableCodeGenerationAttribute)));
		}

		public static bool HasSubtypes(this Type type)
		{
			return type.Assembly.GetTypes().Any(item => (type.IsGenericType && 
				item.BaseType?.IsGenericType == true ? item.BaseType.GetGenericTypeDefinition() : item.BaseType)  == type);
		}

		public static bool IsList(this Type type)
		{
			return type.IsArray || type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type) && !type.IsDictionary();
		}

		public static bool IsDictionary(this Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type) && type.GetGenericArguments().Length == 2;
		}

		public static Type GetListElementsType(this Type type)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
			{
				return type.GetGenericArguments().FirstOrDefault();
			}
			return null;
		}

		public static IEnumerable<Type> GetDependentTypes(this Type type, bool recursive = false, bool includeSystemTypes = false)
		{
			var result = new HashSet<Type>();
			GetDependentTypes(type, result, recursive, includeSystemTypes);
			return result;
		}

		public static void GetDependentTypes(this Type type, HashSet<Type> result, bool recursive = false, bool includeSystemTypes = false)
		{
			var dependencies = new HashSet<Type>();
			if (type.IsApiController())
			{
				foreach (var method in type.GetControllerActions())
				{
					FlattenType(method.ReturnType, dependencies);
					foreach (var parameter in method.GetParameters())
					{
						FlattenType(parameter.ParameterType, dependencies);
					}
				}
			}
			else
			{
				FlattenType(type, dependencies);
				if (type.BaseType != null)
					FlattenType(type.BaseType, dependencies);
				foreach (var property in type.GetDataMembers())
				{
					FlattenType(property.PropertyType, dependencies);
				}

			}
			foreach (var item in dependencies)
			{
				if (item == type || item.Namespace == null)
				{
					continue;
				}
				if (item.IsSystem())
				{
					if (includeSystemTypes)
					{
						result.Add(item);
					}
					continue;
				}
				if (item.IsArray)
				{
					continue;
				}
				var nextType = item.IsGenericType ? item.GetGenericTypeDefinition() : item;
				var added = result.Add(nextType);
				if (recursive && added)
				{
					nextType.GetDependentTypes(result, true);
				}
			}
		}

		private static void FlattenType(this Type type, HashSet<Type> result)
		{
			if (type.IsGenericParameter)
			{
				return;
			}
			result.Add(type);
			if (type.IsArray)
			{
				FlattenType(type.GetElementType(), result);
				return;
			}
			foreach (var subtype in type.GetGenericArguments())
			{
				if (!subtype.IsGenericParameter)
				{
					FlattenType(subtype, result);
				}
			}
		}

		public static bool IsSystem(this Type type)
		{
			return type.Namespace?.StartsWith("System") == true || type.Namespace?.StartsWith("Microsoft") == true;
		}

		public static bool IsNullable(this Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		public static bool CanBeNull(this PropertyInfo propertyInfo)
		{
			return !propertyInfo.IsDefined(typeof(RequiredAttribute));
		}

		public static bool CanBeNull(this ParameterInfo parameterInfo)
		{
			return !parameterInfo.IsDefined(typeof(RequiredAttribute));
		}

		public static bool CanGenericArgumentsBeNull(this PropertyInfo propertyInfo)
		{
			return !propertyInfo.IsDefined(typeof(GenericArgumentsNotNullAttribute)) && !propertyInfo.PropertyType.IsDefined(typeof(GenericArgumentsNotNullAttribute));
		}

		public static bool CanGenericArgumentsBeNull(this Type type)
		{
			return !type.IsDefined(typeof(GenericArgumentsNotNullAttribute));
		}

		public static bool IsSerializableInAndroid(this Type type)
		{
			return type.IsDefined(typeof(SerializableInAndroidAttribute));
		}

		public static string ToCamelCase(this string upperCamelCaseString)
		{
			return new CamelCaseNamingStrategy().GetPropertyName(upperCamelCaseString, false);
		}

		public static string ToFirstUpper(this string s)
		{
			return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
		}

		public static string ToSnakeCase(this string upperCamelCaseString, bool useUpperLetters = true)
		{
			var result = new SnakeCaseNamingStrategy().GetPropertyName(upperCamelCaseString, false);
			return useUpperLetters ? result.ToUpperInvariant() : result.ToLowerInvariant();
		}
	}
}

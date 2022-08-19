using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using CodeGeneration.ApiClientsCodeGenerator.Templates.Android.Kotlin;
using Microsoft.Extensions.Logging;
using static CodeGeneration.ApiClientsCodeGenerator.ActionQueryParameter;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Android
{
	internal class KotlinAndroidConverter : BaseAndroidConverter
	{
		public KotlinAndroidConverter(AndroidSettings settings, ILogger logger) : base(settings, logger)
		{
		}

		public override void CreateAuxiliaryFiles(IEnumerable<Type> types)
		{
			var clientsPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network"));
			Save(clientsPackage, "BaseApiNetworkClient.kt", new BaseApiNetworkClientTemplate(this).TransformText());

			var adaptersPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network.adapters"));
			Save(adaptersPackage, "UnixDateAdapter.kt", new UnixDateAdapterTemplate(this).TransformText());
			Save(adaptersPackage, "ByteArrayAdapter.kt", new ByteArrayAdapterTemplate(this).TransformText());

			var otherPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network.other"));
			Save(clientsPackage, "ApiNetworkClientConfiguration.kt", new ApiNetworkClientConfigurationTemplate(this).TransformText());
			Save(otherPackage, "HttpRequestException.kt", new HttpRequestExceptionTemplate(this).TransformText());

			var interfacesPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("interfaces"));
			Save(interfacesPackage, "RequestCallback.kt", new RequestCallbackTemplate(this).TransformText());
		}

		protected override void ConvertEnum(Type type)
		{
			Save(GetPackage(type), GetTypeName(type) + ".kt", new EnumTemplate(this, type).TransformText());
		}

		protected override void ConvertClass(Type type)
		{
			var imports = GetImports(type.GetDependentTypes().Where(item => item.Namespace != type.Namespace));
			Save(GetPackage(type), GetTypeName(type, false, includeGenericParameters: false) + ".kt", new ClassTemplate(this, type, imports).TransformText());
		}

		protected override void ConvertController(Type type)
		{
			var imports = GetImports(type.GetDependentTypes().Where(item => item.Namespace != type.Namespace));
			Save(GetPackage(type).Append(new AndroidPackage("network")), type.GetControllerName() + "ApiNetworkClient.kt", new ApiNetworkClientTemplate(this, type, imports).TransformText());
			Save(GetPackage(type).Append(new AndroidPackage("interfaces")), type.GetControllerName() + "ApiClient.kt", new ApiClientTemplate(this, type, imports).TransformText());
		}

		internal IEnumerable<string> GetImports(IEnumerable<Type> types)
		{
			return types.Select(item => GetPackage(item).ToString() + "." + GetTypeName(item, false, false, false)).OrderBy(item => item).Distinct();
		}

		internal string GetTypeName(Type type, bool canBeNull = true, bool canGenericArgumentsBeNull = true, bool includeGenericParameters = true)
		{
			if (type.IsGenericParameter)
				return type.Name;
			canGenericArgumentsBeNull &= type.CanGenericArgumentsBeNull();
			if (type.IsArray)
			{
				return GetArrayTypeName(type.GetElementType(), canBeNull, canGenericArgumentsBeNull);
			}
			if (type.IsGenericType && !type.IsNullable())
			{
				var genericArguments = type.GetGenericArguments().Select(item => GetTypeName(item, canGenericArgumentsBeNull));
				if (type.IsDictionary())
				{
					return "Map" + (includeGenericParameters ? "<" + genericArguments.First() + ", " + genericArguments.Last() + ">" : "");
				}
				if (type.IsList())
					return GetArrayTypeName(type.GetGenericArguments().FirstOrDefault(), canBeNull, canGenericArgumentsBeNull);
				var genericType = type.GetGenericTypeDefinition();
				var name = genericType.Name;
				var charIndex = name.IndexOf('`');
				if (charIndex >= 0)
					name = name.Substring(0, charIndex);
				if (!includeGenericParameters)
					return RemoveNamespaces(name);
				return RemoveNamespaces(name) + "<" + string.Join(", ", genericArguments) + ">" + (!canBeNull ? "" : "?");
			}
			if (type.IsNullable())
			{
				var underlyingType = Nullable.GetUnderlyingType(type);
				return GetTypeName(underlyingType) + (!canBeNull ? "" : "?");
			}
			var typesMap = new Dictionary<Type, string>
			{
				{ typeof(sbyte), "Byte" },
				{ typeof(short), "Short" },
				{ typeof(int), "Int" },
				{ typeof(long), "Long" },
				{ typeof(float), "Float" },
				{ typeof(double), "Double" },
				{ typeof(bool), "Boolean" },
				{ typeof(char), "Char" },
				{ typeof(DateTime), "Date" },
				{ typeof(string), "String" },
				{ typeof(decimal), "BigDecimal" },
				{ typeof(BigInteger), "BigInteger" }
			};
			if (typesMap.ContainsKey(type))
			{
				return typesMap[type] + (type.IsValueType || !canBeNull ? "" : "?");
			}
			if (type.IsEnum)
			{
				return type.Name;
			}
			return type.Name + (!canBeNull ? "" : "?");
		}

		internal string GetClassPropertyTypeName(PropertyInfo property)
		{
			var type = property.PropertyType;
			if (type.IsGenericParameter)
				return property.CanBeNull() ? type.Name + "?" : type.Name;
			return GetTypeName(type, property.CanBeNull(), property.CanGenericArgumentsBeNull());
		}

		internal string GetPropertyDefaultValue(PropertyInfo property)
		{
			var type = property.PropertyType;
			if (property.CanBeNull() && (!type.IsValueType || type.IsNullable()))
				return "null";
			if (type.IsDictionary())
			{
				return "mapOf()";
			}
			if (type.IsList())
			{
				var elementType = type.IsArray ? type.GetElementType() : type.GetGenericArguments().FirstOrDefault();
				var arrayTypeName = GetArrayTypeName(elementType, false, property.CanGenericArgumentsBeNull() && type.IsNullable());
				return arrayTypeName.StartsWith("List") ? "listOf()" : arrayTypeName + "(0)";
			}
			if (!property.CanBeNull() && type.IsNullable())
			{
				type = Nullable.GetUnderlyingType(type);
			}
			if (type.IsEnum)
			{
				return GetTypeName(type) + "." + type.GetEnumValues().Cast<Enum>().FirstOrDefault().ToString().ToSnakeCase();
			}
			if (type == typeof(BigInteger))
			{
				return "BigInteger.ZERO";
			}
			switch (Type.GetTypeCode(type))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
					return "0";
				case TypeCode.Decimal:
					return "BigDecimal.ZERO";
				case TypeCode.Double:
				case TypeCode.Single:
					return "0.0";
				case TypeCode.Boolean:
					return "false";
				case TypeCode.Char:
					return "'\0'";
				case TypeCode.String:
					return "\"\"";
				default:
					return GetTypeName(type, false, property.CanGenericArgumentsBeNull()) + "()";
			}
		}

		internal string GetActionQueryParametersString(IEnumerable<ActionQueryParameter> parameters)
		{
			var result = new StringBuilder("val queryParameters = mutableListOf<Pair<String, String?>>().apply {\r\n\t");
			result.Append(string.Join("\r\n\t", parameters.Select(parameter =>
			{
				var canBeNull = parameter.Path.Any(item => item.CanBeNull);
				var value = string.Join(".", parameter.Path.Select(item => item.Name + (item.CanBeNull ? "?" : "")));			
				var listElementsType = parameter.Type.GetListElementsType();				
				if (listElementsType != null)
				{
					var canListElementBeNull = listElementsType.IsNullable();
					if (canListElementBeNull)
					{
						listElementsType = Nullable.GetUnderlyingType(listElementsType);
					}
					var convertElementString = GetQueryParameterConvertString(new ActionQueryParameter("it", new PathItem("it", canListElementBeNull), listElementsType));
					return $"addAll({value}.map {{ Pair(\"{parameter.Name}\", {convertElementString}) }}{(canBeNull ? " ?: listOf()" : "")})";
				}
				var convertString = GetQueryParameterConvertString(parameter);
				return $"add(Pair(\"{parameter.Name}\", {convertString}))";
			})));
			result.Append("\r\n}");
			return result.ToString();
		}

		private string GetQueryParameterConvertString(ActionQueryParameter parameter)
		{
			var canBeNull = parameter.Path.Any(item => item.CanBeNull);
			var value = string.Join(".", parameter.Path.Select(item => item.Name + (item.CanBeNull ? "?" : ""))).TrimEnd('?');
			var smartCastedValue = string.Join(".", parameter.Path.Select((item, index) => item.Name + (item.CanBeNull && index > 0 ? "?" : ""))).TrimEnd('?');
			string result = null;
			if (parameter.Type == typeof(DateTime))
			{
				result = $"SimpleDateFormat(\"yyyy-MM-dd'T'HH:mm:ss.SSSSSSZ\", Locale.ROOT).format({smartCastedValue})";
			}
			if (parameter.Type == typeof(double) || parameter.Type == typeof(float))
			{
				result = $"DecimalFormat.getInstance(Locale.ROOT).format({smartCastedValue})";
			}
			if (parameter.Type.IsEnum)
			{
				result = $"serialize({smartCastedValue}).replace(\"\\\"\", \"\")";
			}
			if (result != null)
			{
				if (canBeNull)
				{
					result = $"if ({value} != null) " + result + " else null";
				}
				return result;
			}
			if (parameter.Type == typeof(string))
			{
				return value;
			}
			return $"{value}{(parameter.Path.Last().CanBeNull ? "?" : "")}.toString()";
		}

		private string GetArrayTypeName(Type elementType, bool canBeNull = true, bool canElementsBeNull = true)
		{
			var primitiveTypes = new List<Type> {
				typeof(byte), typeof(short), typeof(int), typeof(long),
				typeof(float), typeof(double), typeof(bool), typeof(char),
			};
			if (primitiveTypes.Contains(elementType))
			{
				return GetTypeName(elementType, false) + "Array" + (canBeNull ? "?" : "");
			}
			return "List<" + GetTypeName(elementType, canElementsBeNull) + ">" + (!canBeNull ? "" : "?");
		}

		private string RemoveNamespaces(string name)
		{
			var charIndex = name.LastIndexOf(".");
			if (charIndex != -1)
				name = name.Substring(charIndex + 1);
			return name;
		}
	}
}

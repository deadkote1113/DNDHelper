using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using CodeGeneration.ApiClientsCodeGenerator.Templates.Ios;
using Microsoft.Extensions.Logging;
using static CodeGeneration.ApiClientsCodeGenerator.ActionQueryParameter;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Ios
{
	internal class IosConverter : BaseConverter<IosSettings>
	{
		public IosConverter(IosSettings settings, ILogger logger) : base(settings, logger)
		{
		}

		public override void CreateAuxiliaryFiles(IEnumerable<Type> types)
		{
			Save(Settings.ApiClientsFolder + "/Network", "ApiNetworkClientConfiguration.swift", new ApiNetworkClientConfigurationTemplate(this).TransformText());
			Save(Settings.ApiClientsFolder + "/Network", "BaseApiNetworkClient.swift", new BaseApiNetworkClientTemplate(this).TransformText());
			Save(Settings.ApiClientsFolder + "/Network/Other", "CustomError.swift", new CustomErrorTemplate().TransformText());
			Save(Settings.ApiClientsFolder + "/Network/Other", "HttpRequestError.swift", new HttpRequestErrorTemplate().TransformText());
		}

		public override void CreateResources(IEnumerable<Type> types)
		{
			var resources = types.Where(item => item.IsEnum).SelectMany(item => item.GetEnumValues().Cast<Enum>())
				.ToImmutableSortedDictionary(GetEnumResourceName, item => item.GetDisplayName()).ToList();
			if (resources.Any())
			{
				Save("Enums", "Enums.strings", new StringsTemplate(resources).TransformText());
			}
		}

		protected override void ConvertEnum(Type type)
		{
			Save(Settings.EnumsFolder, GetTypeName(type, false) + ".swift", new EnumTemplate(this, type).TransformText());
		}

		protected override void ConvertClass(Type type)
		{
			Save(GetFolder(type), GetTypeName(type, false, includeGenericParameters: false) + ".swift", new ClassTemplate(this, type).TransformText());
		}

		protected override void ConvertController(Type type)
		{
			Save(Settings.ApiClientsFolder + "/Protocols", type.GetControllerName() + "ApiClient.swift", new ApiClientTemplate(this, type).TransformText());
			Save(Settings.ApiClientsFolder + "/Network", type.GetControllerName() + "ApiNetworkClient.swift", new ApiNetworkClientTemplate(this, type).TransformText());
		}

		public override void Convert(IEnumerable<Type> types)
		{
			var typesList = types.ToList();
			base.Convert(typesList);
			foreach (var typesGroup in typesList.GroupBy(type => type.Name))
			{
				if (typesGroup.Count() > 1)
				{
					var namespaces = string.Join(", ", typesGroup.Select(item => item.Namespace));
					Logger?.LogWarning($"Найдено несколько типов с именем {typesGroup.Key} (в пространствах имен {namespaces}). Это приведет к невозможности компиляции кода на языке Swift.");
				}
			}
		}

		internal string GetEnumResourceName(Enum value)
		{
			return $"{value.GetType().Name}_{value}";
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
					return "[" + genericArguments.First() + ": " + genericArguments.Last() + "]";
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
				{ typeof(sbyte), "Int8" },
				{ typeof(byte), "UInt8" },
				{ typeof(int), "Int" },
				{ typeof(uint), "UInt" },
				{ typeof(long), "Int64" },
				{ typeof(ulong), "UInt64" },
				{ typeof(float), "Float" },
				{ typeof(double), "Double" },
				{ typeof(decimal), "Decimal" },
				{ typeof(bool), "Bool" },
				{ typeof(char), "Character" },
				{ typeof(DateTime), "Date" },
				{ typeof(string), "String" },
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
				return "nil";
			if (!property.CanBeNull() && type.IsNullable())
			{
				type = Nullable.GetUnderlyingType(type);
			}
			if (type.IsEnum)
			{
				return "." + type.GetEnumValues().Cast<Enum>().FirstOrDefault().ToString().ToCamelCase();
			}
			if (type.IsDictionary())
			{
				return "[:]";
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
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return "0";
				case TypeCode.Boolean:
					return "false";
				case TypeCode.Char:
					return "\"\0\"";
				case TypeCode.String:
					return "\"\"";
				default:
					return GetTypeName(type, false, property.CanGenericArgumentsBeNull()) + "()";
			}
		}

		internal string GetActionQueryParametersString(IEnumerable<ActionQueryParameter> parameters)
		{
			var result = new StringBuilder("var queryParameters = [(String, String?)]()\r\n");
			result.Append(string.Join("\r\n", parameters.Select(parameter =>
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
					var convertElementString = GetQueryParameterConvertString(new ActionQueryParameter("$0", new PathItem("$0", canListElementBeNull), listElementsType));
					return $"queryParameters.append(contentsOf: {value}.map {{ (\"{parameter.Name}\", {convertElementString}) }}{(canBeNull ? " ?? [(String, String?)]()" : "")})";
				}
				var convertString = GetQueryParameterConvertString(parameter);
				return $"queryParameters.append((\"{parameter.Name}\", {convertString}))";
			})));
			return result.ToString();
		}

		private string GetQueryParameterConvertString(ActionQueryParameter parameter)
		{
			var canBeNull = parameter.Path.Any(item => item.CanBeNull);
			var value = string.Join(".", parameter.Path.Select(item => item.Name + (item.CanBeNull ? "?" : ""))).TrimEnd('?');
			var nonNullValue = string.Join(".", parameter.Path.Select(item => item.Name + (item.CanBeNull ? "!" : "")));
			if (parameter.Type.IsEnum)
			{
				return $"{value}{(parameter.Path.Last().CanBeNull ? "?" : "")}.rawValue";
			}
			if (parameter.Type == typeof(string))
			{
				return value;
			}
			string result = $"String(describing: {nonNullValue})";
			if (parameter.Type == typeof(DateTime))
			{
				result = $"ISO8601DateFormatter().string(from: {nonNullValue})";
			}
			if (parameter.Type == typeof(double) || parameter.Type == typeof(float))
			{
				result = $"String(describing: {nonNullValue})";
			}
			if (canBeNull)
			{
				result = $"{value} != nil ? " + result + " : nil";
			}
			return result;
		}

		private string GetArrayTypeName(Type elementType, bool canBeNull = true, bool canElementsBeNull = true)
		{
			if (elementType == typeof(byte))
			{
				return canBeNull ? "Data?" : "Data";
			}
			return "[" + GetTypeName(elementType, canElementsBeNull) + "]" + (!canBeNull ? "" : "?");
		}

		private string RemoveNamespaces(string name)
		{
			var charIndex = name.LastIndexOf(".");
			if (charIndex != -1)
				name = name.Substring(charIndex + 1);
			return name;
		}

		private string GetFolder(Type type)
		{
			if (type.IsEnum)
				return Settings.EnumsFolder;
			if (type.IsApiController())
				return Settings.ApiClientsFolder;
			var name = type.Namespace;
			var prefix = type.Assembly.GetName().Name;
			if (name.StartsWith(prefix))
				name = name.Substring(prefix.Length).TrimStart('.');
			return name.Replace(".", "\\");
		}

		private void Save(string folderPath, string fileName, string content)
		{
			var directoryPath = Path.Combine(Settings.OutputDirectory, folderPath);
			Directory.CreateDirectory(directoryPath);
			var encoding = new UTF8Encoding(false);
			using (var stream = new StreamWriter(Path.Combine(directoryPath, fileName), false, encoding))
			{
				stream.Write(content.Replace("\r\n", "\n"));
			}
		}
	}
}
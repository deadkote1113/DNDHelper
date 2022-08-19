using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.IO;
using System.Text;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using CodeGeneration.ApiClientsCodeGenerator.Templates.Flutter;
using Microsoft.Extensions.Logging;
using static CodeGeneration.ApiClientsCodeGenerator.ActionQueryParameter;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Flutter
{
	internal class FlutterConverter : BaseConverter<FlutterSettings>
	{
		private static string RootFolder => "lib/src/";

		public FlutterConverter(FlutterSettings settings, ILogger logger) : base(settings, logger)
		{
		}

		public override void CreateAuxiliaryFiles(IEnumerable<Type> types)
		{
			Save("", "build.yaml", new BuildConfigurationTemplate(this).TransformText());

			var apiClientsFolder = RootFolder + Settings.ApiClientsFolder;

			var apiNetworkClientsFolder = apiClientsFolder + "/network";
			Save(apiNetworkClientsFolder, "api_network_client_configuration.dart", new ApiNetworkClientConfigurationTemplate(this).TransformText());
			Save(apiNetworkClientsFolder, "base_api_network_client.dart", new BaseApiNetworkClientTemplate(this).TransformText());

			var convertersFolder = apiNetworkClientsFolder + "/converters";
			Save(convertersFolder, "date_time_unix_converter.dart", new DateTimeUnixConverterTemplate(this).TransformText());
			Save(convertersFolder, "nullable_date_time_unix_converter.dart", new NullableDateTimeUnixConverterTemplate(this).TransformText());
			Save(convertersFolder, "uint8_list_converter.dart", new Uint8ListConverterTemplate(this).TransformText());
			Save(convertersFolder, "nullable_uint8_list_converter.dart", new NullableUint8ListConverterTemplate(this).TransformText());
			Save(convertersFolder, "date_time_list_unix_converter.dart", new DateTimeListUnixConverterTemplate(this).TransformText());

			var otherFolder = apiNetworkClientsFolder + "/other";
			Save(otherFolder, "http_request_exception.dart", new HttpRequestExceptionTemplate().TransformText());

			var exports = new List<string> { 
				$"export \"src/{Settings.ApiClientsFolder}/network/api_network_client_configuration.dart\";",
				$"export \"src/{Settings.ApiClientsFolder}/network/other/http_request_exception.dart\";"
			};
			exports.AddRange(types.SelectMany(item =>
			{
				var prefix = "export \"src/" + GetFolder(item)[RootFolder.Length..].Replace("\\", "/") + "/";
				if (item.IsApiController())
				{
					return new[] { 
						prefix + "interfaces/" + item.GetControllerName().ToSnakeCase(false) + "_api_client.dart\";",
						prefix + "network/" + item.GetControllerName().ToSnakeCase(false) + "_api_network_client.dart\";"
					};
				}
				return new[] { prefix + GetFileName(item) + "\";" };
			}));
			exports.Sort();
			Save("lib", Settings.PackageName + ".dart", string.Join("\r\n", exports));
		}

		public override void CreateResources(IEnumerable<Type> types)
		{
			
		}

		protected override void ConvertEnum(Type type)
		{
			Save(GetFolder(type), GetFileName(type), new EnumTemplate(this, type).TransformText());
		}

		protected override void ConvertClass(Type type)
		{
			var imports = GetImports(type.GetDependentTypes(includeSystemTypes: true).Where(item => item != type.BaseType && (!type.BaseType.IsGenericType || item != type.BaseType?.GetGenericTypeDefinition())));
			Save(GetFolder(type), GetFileName(type), new ClassTemplate(this, type, imports).TransformText());
		}

		protected override void ConvertController(Type type)
		{
			var imports = GetImports(type.GetDependentTypes(includeSystemTypes: true));
			Save(GetFolder(type) + "/interfaces", type.GetControllerName().ToSnakeCase(false) + "_api_client.dart", new ApiClientTemplate(this, type, imports).TransformText());
			Save(GetFolder(type) + "/network", type.GetControllerName().ToSnakeCase(false) + "_api_network_client.dart", new ApiNetworkClientTemplate(this, type, imports).TransformText());
		}

		internal IEnumerable<string> GetImports(IEnumerable<Type> types)
		{
			return types.Select(item =>
			{
				if (item == typeof(decimal))
				{
					return "package:decimal/decimal.dart";
				}
				if (item.GetListElementsType() == typeof(byte))
				{
					return "dart:typed_data";
				}
				if (item.IsSystem())
				{
					return null;
				}
				return $"package:{Settings.PackageName}/src/{GetFolder(item)[RootFolder.Length..].Replace("\\", "/")}/{GetFileName(item)}";
			}).Where(item => item != null).OrderBy(item => item).Distinct();
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
				{ typeof(sbyte), "int" },
				{ typeof(short), "int" },
				{ typeof(int), "int" },
				{ typeof(long), "int" },
				{ typeof(float), "double" },
				{ typeof(double), "double" },
				{ typeof(decimal), "Decimal" },
				{ typeof(bool), "bool" },
				{ typeof(char), "String" },
				{ typeof(DateTime), "DateTime" },
				{ typeof(string), "String" }
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

		internal string GetActionQueryParametersString(IEnumerable<ActionQueryParameter> parameters)
		{
			var result = new StringBuilder("final queryParameters = <MapEntry<String, String?>>[]\r\n  ");
			result.Append(string.Join("\r\n  ", parameters.Select(parameter =>
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
					var convertElementString = GetQueryParameterConvertString(new ActionQueryParameter("item", new PathItem("item", canListElementBeNull), listElementsType));
					return $"..addAll({value}.map((item) => MapEntry(\"{parameter.Name}\", {convertElementString})){(canBeNull ? " ?? []" : "")})";
				}
				var convertString = GetQueryParameterConvertString(parameter);
				return $"..add(MapEntry(\"{parameter.Name}\", {convertString}))";
			})));
			result.Append(";");
			return result.ToString();
		}

		private string GetQueryParameterConvertString(ActionQueryParameter parameter)
		{
			var value = string.Join(".", parameter.Path.Select(item => item.Name + (item.CanBeNull ? "?" : "")));
			if (parameter.Type == typeof(DateTime))
			{
				return $"{value}.toIso8601String()";
			}
			if (parameter.Type.IsEnum)
			{
				return $"{value}.jsonValue";
			}
			if (parameter.Type == typeof(string))
			{
				return value.TrimEnd('?');
			}
			return $"{value}.toString()";
		}

		private string GetArrayTypeName(Type elementType, bool canBeNull = true, bool canElementsBeNull = true)
		{
			if (elementType == typeof(byte))
			{
				return "Uint8List" + (canBeNull ? "?" : "");
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

		private string GetFolder(Type type)
		{
			if (type.IsEnum)
				return RootFolder + Settings.EnumsFolder;
			if (type.IsApiController())
				return RootFolder + Settings.ApiClientsFolder;
			var name = type.Namespace;
			var prefix = type.Assembly.GetName().Name;
			if (name.StartsWith(prefix))
				name = name.Substring(prefix.Length).TrimStart('.');
			return RootFolder + string.Join("\\", name.Split('.').Select(item => item.ToSnakeCase(false)));
		}

		private string GetFileName(Type type)
		{
			return GetTypeName(type, false, includeGenericParameters: false).ToSnakeCase(false) + ".dart";
		}

		private void Save(string folderPath, string fileName, string content)
		{
			var directoryPath = Path.Combine(Settings.OutputDirectory, folderPath);
			Directory.CreateDirectory(directoryPath);
			var encoding = new UTF8Encoding(false);
			using (var stream = new StreamWriter(Path.Combine(directoryPath, fileName), false, encoding))
			{
				stream.Write(content);
			}
		}
	}
}

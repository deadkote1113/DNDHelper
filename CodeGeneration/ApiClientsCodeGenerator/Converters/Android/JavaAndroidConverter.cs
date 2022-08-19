using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using CodeGeneration.ApiClientsCodeGenerator.Templates.Android.Java;
using Microsoft.Extensions.Logging;
using static CodeGeneration.ApiClientsCodeGenerator.ActionQueryParameter;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Android
{
	internal class JavaAndroidConverter : BaseAndroidConverter
	{
		public JavaAndroidConverter(AndroidSettings settings, ILogger logger) : base(settings, logger)
		{
		}

		public override void CreateAuxiliaryFiles(IEnumerable<Type> types)
		{
			var clientsPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network"));
			Save(clientsPackage, "BaseApiNetworkClient.java", new BaseApiNetworkClientTemplate(this).TransformText());

			var adaptersPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network.adapters"));
			Save(adaptersPackage, "UnixDateAdapter.java", new UnixDateAdapterTemplate(this).TransformText());
			Save(adaptersPackage, "ByteArrayAdapter.java", new ByteArrayAdapterTemplate(this).TransformText());

			var otherPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("network.other"));
			Save(clientsPackage, "ApiNetworkClientConfiguration.java", new ApiNetworkClientConfigurationTemplate(this).TransformText());
			Save(otherPackage, "HttpRequestException.java", new HttpRequestExceptionTemplate(this).TransformText());

			var interfacesPackage = Settings.ApiClientsPackage.Append(new AndroidPackage("interfaces"));
			Save(interfacesPackage, "RequestCallback.java", new RequestCallbackTemplate(this).TransformText());
		}

		protected override void ConvertEnum(Type type)
		{
			Save(GetPackage(type), GetTypeName(type) + ".java", new EnumTemplate(this, type).TransformText());
		}

		protected override void ConvertClass(Type type)
		{
			var imports = GetImports(type.GetDependentTypes().Where(item => item.Namespace != type.Namespace));
			Save(GetPackage(type), GetTypeName(type, false) + ".java", new ClassTemplate(this, type, imports).TransformText());
		}

		protected override void ConvertController(Type type)
		{
			var imports = GetImports(type.GetDependentTypes().Where(item => item.Namespace != type.Namespace));
			Save(GetPackage(type).Append(new AndroidPackage("network")), type.GetControllerName() + "ApiNetworkClient.java", new ApiNetworkClientTemplate(this, type, imports).TransformText());
			Save(GetPackage(type).Append(new AndroidPackage("interfaces")), type.GetControllerName() + "ApiClient.java", new ApiClientTemplate(this, type, imports).TransformText());
		}

		internal IEnumerable<string> GetImports(IEnumerable<Type> types)
		{
			return types.Select(item => GetPackage(item).ToString() + "." + GetTypeName(item, false)).OrderBy(item => item).Distinct();
		}

		internal string GetTypeName(Type type, bool includeGenericParameters = true)
		{
			if (type.IsGenericParameter)
				return type.Name;
			if (type.IsArray)
			{
				return GetArrayTypeName(type.GetElementType());
			}
			if (type.IsGenericType && !type.IsNullable())
			{
				var genericArguments = type.GetGenericArguments().Select(item => GetTypeName(item));
				if (typeof(IDictionary).IsAssignableFrom(type) && genericArguments.Count() == 2)
				{
					return "Map" + (includeGenericParameters ? "<" + genericArguments.First() + ", " + genericArguments.Last() + ">" : "");
				}
				if (type.IsList())
					return GetArrayTypeName(type.GetGenericArguments().FirstOrDefault());
				var genericType = type.GetGenericTypeDefinition();
				var name = genericType.Name;
				var charIndex = name.IndexOf('`');
				if (charIndex >= 0)
					name = name.Substring(0, charIndex);
				if (!includeGenericParameters)
					return RemoveNamespaces(name);
				return RemoveNamespaces(name) + "<" + string.Join(", ", genericArguments) + ">";
			}
			var typesMap = new Dictionary<Type, string>
			{
				{ typeof(sbyte), "byte" },
				{ typeof(short), "short" },
				{ typeof(int), "int" },
				{ typeof(long), "long" },
				{ typeof(float), "float" },
				{ typeof(double), "double" },
				{ typeof(bool), "boolean" },
				{ typeof(char), "char" },
				{ typeof(DateTime), "Date" },
				{ typeof(decimal), "BigDecimal" },
				{ typeof(sbyte?), "Byte" },
				{ typeof(short?), "Short" },
				{ typeof(int?), "Integer" },
				{ typeof(long?), "Long" },
				{ typeof(float?), "Float" },
				{ typeof(double?), "Double" },
				{ typeof(bool?), "Boolean" },
				{ typeof(char?), "Character" },
				{ typeof(decimal?), "BigDecimal" },
				{ typeof(DateTime?), "Date" },
				{ typeof(string), "String" },
				{ typeof(BigInteger), "BigInteger" }
			};
			if (typesMap.ContainsKey(type))
			{
				return typesMap[type];
			}
			var underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return RemoveNamespaces(underlyingType.Name);
			}
			return type.Name;
		}

		private string GetArrayTypeName(Type elementType)
		{
			if (elementType == typeof(byte))
			{
				return "byte[]";
			}
			var primitiveTypes = new List<Type> {
				typeof(sbyte), typeof(short), typeof(int), typeof(long),
				typeof(float), typeof(double), typeof(bool), typeof(char),
			};
			if (primitiveTypes.Contains(elementType))
			{
				return GetTypeName(elementType, false) + "[]";
			}
			return "List<" + GetTypeName(elementType) + ">";
		}

		internal string GetActionQueryParametersString(IEnumerable<ActionQueryParameter> parameters)
		{
			var result = new StringBuilder("List<Pair<String, String>> queryParameters = new ArrayList<>();\r\n");
			result.Append(string.Join("\r\n", parameters.Select(parameter =>
			{
				var canBeNull = parameter.Path.Any(item => item.CanBeNull);
				var value = string.Join(".", parameter.Path.Select(item => item.Name));
				var listElementsType = parameter.Type.GetListElementsType();
				var parameterResult = "";
				if (listElementsType != null)
				{
					var canListElementBeNull = listElementsType.IsNullable();
					if (canListElementBeNull)
					{
						listElementsType = Nullable.GetUnderlyingType(listElementsType);
					}
					var convertElementString = GetQueryParameterConvertString(new ActionQueryParameter("item", new PathItem("item", canListElementBeNull), listElementsType));
					parameterResult = $"for ({GetTypeName(listElementsType, true)} item: {value}) {{\r\n\tqueryParameters.add(new Pair<>(\"{parameter.Name}\", {(canListElementBeNull ? "item != null ? " : "") + convertElementString + (canListElementBeNull ? " : null" : "")}));\r\n}}";
				}
				else
				{
					var convertString = GetQueryParameterConvertString(parameter);
					parameterResult = $"queryParameters.add(new Pair<>(\"{parameter.Name}\", {convertString}));";
				}
				if (canBeNull)
				{
					var checkConditions = new List<string>();
					var currentPath = new StringBuilder();
					foreach (var pathItem in parameter.Path)
					{
						currentPath.Append((currentPath.Length > 0 ? "." : "") + pathItem.Name);
						if (pathItem.CanBeNull)
						{
							checkConditions.Add(currentPath.ToString() + " != null");
						}
					}
					return $"if ({string.Join(" && ", checkConditions)}) {{\r\n\t{parameterResult.Replace("\r\n", "\r\n\t")}\r\n}}";
				}
				else
				{
					return parameterResult;
				}
			})));
			return result.ToString();
		}

		private string GetQueryParameterConvertString(ActionQueryParameter parameter)
		{
			var canBeNull = parameter.Path.Any(item => item.CanBeNull);
			var value = string.Join(".", parameter.Path.Select(item => item.Name));
			string result = $"String.valueOf({value})";
			if (parameter.Type == typeof(string))
			{
				return value;
			}
			if (parameter.Type == typeof(DateTime))
			{
				result = $"new SimpleDateFormat(\"yyyy-MM-dd'T'HH:mm:ss.SSSSSSZ\", Locale.ROOT).format({value})";
			}
			if (parameter.Type == typeof(double) || parameter.Type == typeof(float))
			{
				result = $"DecimalFormat.getInstance(Locale.ROOT).format({value})";
			}
			if (parameter.Type.IsEnum)
			{
				result = $"serialize({value}).replace(\"\\\"\", \"\")";
			}
			return result;
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

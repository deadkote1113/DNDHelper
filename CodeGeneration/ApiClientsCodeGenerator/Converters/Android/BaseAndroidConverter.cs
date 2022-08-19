using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using CodeGeneration.ApiClientsCodeGenerator.Templates.Android;
using Microsoft.Extensions.Logging;

namespace CodeGeneration.ApiClientsCodeGenerator.Converters.Android
{
	internal abstract class BaseAndroidConverter : BaseConverter<AndroidSettings>
	{
		protected BaseAndroidConverter(AndroidSettings settings, ILogger logger) : base(settings, logger)
		{
		}

		public override void CreateResources(IEnumerable<Type> types)
		{
			var resources = types.Where(item => item.IsEnum).SelectMany(item => item.GetEnumValues().Cast<Enum>())
				.ToImmutableSortedDictionary(GetEnumResourceName, item => item.GetDisplayName()).ToList();
			if (resources.Any())
			{
				Save("res/values", "strings.xml", new StringsTemplate(resources).TransformText());
			}
		}

		internal string GetEnumResourceName(Enum value)
		{
			return $"enum_{value.GetType().Name.ToCamelCase()}_{value.ToString().ToCamelCase()}";
		}

		internal AndroidPackage GetPackage(Type type)
		{
			if (type.IsEnum)
				return Settings.EnumsPackage;
			if (type.IsApiController())
				return Settings.ApiClientsPackage;
			var name = type.Namespace;
			var prefix = type.Assembly.GetName().Name;
			if (name.StartsWith(prefix))
				name = name.Substring(prefix.Length).TrimStart('.');
			return Settings.BasePackage.Append(new AndroidPackage(name));
		}

		protected void Save(AndroidPackage package, string fileName, string content)
		{
			var pathItems = new List<string>
			{
				"java"
			};
			pathItems.AddRange(package.GetItems());
			Save(Path.Combine(pathItems.ToArray()), fileName, content);
		}

		protected void Save(string directory, string fileName, string content)
		{
			var fullDirectoryPath = Path.Combine(Settings.OutputDirectory, directory);
			Directory.CreateDirectory(fullDirectoryPath);
			var encoding = new UTF8Encoding(false);
			using var stream = new StreamWriter(Path.Combine(fullDirectoryPath, fileName), false, encoding);
			stream.Write(content);
		}
	}
}

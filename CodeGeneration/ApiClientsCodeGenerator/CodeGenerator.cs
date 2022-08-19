using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeGeneration.ApiClientsCodeGenerator.Converters;
using CodeGeneration.ApiClientsCodeGenerator.Converters.Android;
using CodeGeneration.ApiClientsCodeGenerator.Converters.Flutter;
using CodeGeneration.ApiClientsCodeGenerator.Converters.Ios;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using Microsoft.Extensions.Logging;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	internal class CodeGenerator
	{
		internal ILogger Logger { get; }

		public CodeGenerator(ILogger logger = null)
		{
			Logger = logger;
		}

		public void Generate(GeneratorSettings settings)
		{
			var controllerTypes = settings.Assembly.GetTypes().Where(item => item.IsApiController() && item.GetControllerActions().Any()).ToList();
			var typesSet = new HashSet<Type>(controllerTypes);
			foreach (var controllerType in controllerTypes)
			{
				controllerType.GetDependentTypes(typesSet, true);
			}
			var converters = new BaseConverter[]
			{
				new JavaAndroidConverter(new AndroidSettings
				{
					OutputDirectory = settings.OutputDirectory.TrimEnd('\\') + "\\Android\\Java",
					DebugApiUrl = settings.DebugApiUrl,
					ReleaseApiUrl = settings.ReleaseApiUrl,
					BasePackage = settings.AndroidPackage,
					ApiClientsPackage = settings.AndroidPackage.Append(settings.AndroidTrailingApiClientsPackage),
					EnumsPackage = settings.AndroidPackage.Append(settings.AndroidTrailingEnumsPackage),
					ApiClientMethodTypes = settings.ApiClientMethodTypes
				}, Logger),
				new KotlinAndroidConverter(new AndroidSettings
				{
					OutputDirectory = settings.OutputDirectory.TrimEnd('\\') + "\\Android\\Kotlin",
					DebugApiUrl = settings.DebugApiUrl,
					ReleaseApiUrl = settings.ReleaseApiUrl,
					BasePackage = settings.AndroidPackage,
					ApiClientsPackage = settings.AndroidPackage.Append(settings.AndroidTrailingApiClientsPackage),
					EnumsPackage = settings.AndroidPackage.Append(settings.AndroidTrailingEnumsPackage),
					ApiClientMethodTypes = settings.ApiClientMethodTypes
				}, Logger),
				new IosConverter(new IosSettings
				{
					OutputDirectory = settings.OutputDirectory.TrimEnd('\\') + '\\' + "Ios",
					DebugApiUrl = settings.DebugApiUrl,
					ReleaseApiUrl = settings.ReleaseApiUrl,
					ApiClientsFolder = settings.IosApiClientsFolder,
					EnumsFolder = settings.IosEnumsFolder,
					ApiClientMethodTypes = settings.ApiClientMethodTypes
				}, Logger),
				new FlutterConverter(new FlutterSettings
				{
					OutputDirectory = settings.OutputDirectory.TrimEnd('\\') + '\\' + "Flutter",
					DebugApiUrl = settings.DebugApiUrl,
					ReleaseApiUrl = settings.ReleaseApiUrl,
					PackageName = settings.FlutterPackageName,
					ApiClientsFolder = settings.FlutterApiClientsFolder,
					EnumsFolder = settings.FlutterEnumsFolder
				}, Logger)
			};
			foreach (var converter in converters)
			{
				converter.Convert(typesSet);
			}
		}
	}
}

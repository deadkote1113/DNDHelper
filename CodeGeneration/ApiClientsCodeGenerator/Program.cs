using System;
using System.IO;
using CodeGeneration.ApiClientsCodeGenerator.Converters.Android;
using CodeGeneration.ApiClientsCodeGenerator.Settings;
using Microsoft.Extensions.Logging;
using Api.Enums;
using Api.Versioning;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var outputDirectory = "output";
			if (Directory.Exists(outputDirectory))
			{
				try
				{
					Directory.Delete(outputDirectory, true);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Невозможно очистить директорию {outputDirectory}: {ex}");
				}
			}
			using var loggerFactory =
				LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));
			new CodeGenerator(loggerFactory.CreateLogger<Program>()).Generate(new GeneratorSettings
			{
				Assembly = typeof(OperationStatus).Assembly,
				OutputDirectory = outputDirectory,
				DebugApiUrl = $"http://192.168.0.26/MyFfinApi/v{ApiVersionRangeAttribute.CurrentApiVersion}/",
				ReleaseApiUrl = $"https://myffin.spaceapp.ru/v{ApiVersionRangeAttribute.CurrentApiVersion}/",
				AndroidPackage = new AndroidPackage("ru.spaceapp.myffin.android.sal"),
				AndroidTrailingApiClientsPackage = new AndroidPackage("apiclients"),
				AndroidTrailingEnumsPackage = new AndroidPackage("enums"),
				IosApiClientsFolder = "ApiClients",
				IosEnumsFolder = "Enums",
				FlutterApiClientsFolder = "api_clients",
				FlutterEnumsFolder = "enums",
				FlutterPackageName = "sal",
				ApiClientMethodTypes = ApiClientMethodType.All
			});
		}
	}
}

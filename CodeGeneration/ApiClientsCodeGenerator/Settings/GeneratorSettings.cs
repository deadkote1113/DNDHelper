using System.Reflection;
using CodeGeneration.ApiClientsCodeGenerator.Converters.Android;

namespace CodeGeneration.ApiClientsCodeGenerator.Settings
{
	internal class GeneratorSettings
	{
		public Assembly Assembly { get; set; }
		public string OutputDirectory { get; set; }
		public string DebugApiUrl { get; set; }
		public string ReleaseApiUrl { get; set; }
		public AndroidPackage AndroidPackage { get; set; }
		public AndroidPackage AndroidTrailingEnumsPackage { get; set; }
		public AndroidPackage AndroidTrailingApiClientsPackage { get; set; }
		public string IosEnumsFolder { get; set; }
		public string IosApiClientsFolder { get; set; }
		public string FlutterPackageName { get; set; }
		public string FlutterEnumsFolder { get; set; }
		public string FlutterApiClientsFolder { get; set; }
		public ApiClientMethodType ApiClientMethodTypes { get; set; }
	}
}

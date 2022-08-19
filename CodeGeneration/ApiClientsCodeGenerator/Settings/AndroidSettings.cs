using CodeGeneration.ApiClientsCodeGenerator.Converters.Android;

namespace CodeGeneration.ApiClientsCodeGenerator.Settings
{
	internal class AndroidSettings : ConverterSettings
	{
		public AndroidPackage BasePackage { get; set; }
		public AndroidPackage EnumsPackage { get; set; }
		public AndroidPackage ApiClientsPackage { get; set; }
		public ApiClientMethodType ApiClientMethodTypes { get; set; }
	}
}

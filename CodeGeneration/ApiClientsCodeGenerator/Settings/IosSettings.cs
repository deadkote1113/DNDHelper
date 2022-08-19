namespace CodeGeneration.ApiClientsCodeGenerator.Settings
{
	internal class IosSettings: ConverterSettings
	{
		public string EnumsFolder { get; set; }
		public string ApiClientsFolder { get; set; }
		public ApiClientMethodType ApiClientMethodTypes { get; set; }
	}
}

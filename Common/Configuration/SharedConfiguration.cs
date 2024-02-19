using Common.Configuration.Mail;
using Microsoft.Extensions.Logging;

namespace Common.Configuration
{
	public class SharedConfiguration
	{
		public static string DbConnectionString { get; private set; }

		public static SmtpConfiguration SmtpConfiguration { get; private set; }

		public static ILoggerFactory LoggerFactory { get; private set; }

		public const int DefaultReaderId = 1;

		public static void UpdateSharedConfiguration(string dbConnectionString, SmtpConfiguration smtpConfiguration, ILoggerFactory loggerFactory)
		{
			DbConnectionString = dbConnectionString;
			SmtpConfiguration = smtpConfiguration;
			LoggerFactory = loggerFactory;
		}
	}
}

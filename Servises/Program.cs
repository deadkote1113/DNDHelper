using Common.Configuration;
using Common.Configuration.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Servises.BackGround.TelegramServises;

namespace Servises;

internal class Program
{
	public static void Main(string[] args)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		var host = CreateHostBuilder(args).Build();
		var configuration = host.Services.GetService<IConfiguration>();
		var loggerFactory = host.Services.GetService<ILoggerFactory>();

		SharedConfiguration.UpdateSharedConfiguration(
			configuration.GetConnectionString("DefaultConnectionString"),
			configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>(),
			loggerFactory);

		host.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddLogging(builder =>
				{
					builder.AddNLog();
				});
				services.AddHostedService<TelegramBotServise>();
			}
			);
}
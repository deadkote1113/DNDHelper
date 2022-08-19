using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Authentication;
using Api.Versioning;
using Common.Configuration;
using Common.Configuration.Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnixDateTimeConverter = Tools.Serialization.Converters.UnixDateTimeConverter;

namespace Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(options =>
			{
			}).AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				};
				options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
				options.SerializerSettings.Converters.Add(new UnixDateTimeConverter());
				options.SerializerSettings.Converters.Add(new StringEnumConverter());
			}).ConfigureApiBehaviorOptions(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			services.AddApiVersioning(options =>
			{
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
				options.ErrorResponses = new ApiVersionErrorProvider();
			});

			services.AddAuthentication(DefaultApiAuthenticationOptions.DefaultScheme).AddDefaultApiAuthentication();

			services.AddAuthorization();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
		{
			SharedConfiguration.UpdateSharedConfiguration(configuration.GetConnectionString("DefaultConnectionString"),
				configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>(), loggerFactory);

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Api.Enums;
using Api.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;

namespace Api.Authentication
{
	public class DefaultApiAuthenticationHandler : AuthenticationHandler<DefaultApiAuthenticationOptions>
	{
		private readonly JsonSerializerSettings serializerSettings;

		public DefaultApiAuthenticationHandler(IOptionsMonitor<DefaultApiAuthenticationOptions> options, 
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptions<MvcNewtonsoftJsonOptions> serializerOptions) : base(options, logger, encoder, clock)
		{
			this.serializerSettings = serializerOptions.Value.SerializerSettings;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string token = null;
			var requesr = Request;
			if (Request.Headers.ContainsKey("Authorization"))
			{
				token = Request.Headers["Authorization"].ToString();
				if (token.StartsWith("Bearer "))
				{
					token = token.Substring(7);
				}
			}
			if (token == null)
			{
				return AuthenticateResult.NoResult();
			}
			var user = await TokenProcessor.GetUser(token);
			if (user == null)
			{
				return AuthenticateResult.Fail("Incorrect token");
			}
			if (user.IsBlocked)
			{
				return AuthenticateResult.Fail("User is blocked");
			}
			return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ApiUserIdentity(user)), 
				DefaultApiAuthenticationOptions.DefaultScheme));
		}

		protected override Task HandleChallengeAsync(AuthenticationProperties properties)
		{
			return HandleForbiddenAsync(properties);
		}

		protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
		{
			Response.StatusCode = 403;
			await Response.WriteAsync(JsonConvert.SerializeObject(new ResponseWrapper<EmptyResponse>(OperationStatus.Forbidden),
				serializerSettings));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Api.Authentication
{
	public static class DefaultApiAuthenticationExtensions
	{
		public static AuthenticationBuilder AddDefaultApiAuthentication(this AuthenticationBuilder builder, Action<DefaultApiAuthenticationOptions> configureOptions = null)
		{
			return builder.AddScheme<DefaultApiAuthenticationOptions, DefaultApiAuthenticationHandler>(DefaultApiAuthenticationOptions.DefaultScheme, configureOptions ?? (options =>
			{
			}));
		}
	}
}

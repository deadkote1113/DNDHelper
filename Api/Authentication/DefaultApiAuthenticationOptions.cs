using Microsoft.AspNetCore.Authentication;

namespace Api.Authentication
{
	public class DefaultApiAuthenticationOptions : AuthenticationSchemeOptions
	{
		public const string DefaultScheme = "DefaultApiAuthentication";
	}
}
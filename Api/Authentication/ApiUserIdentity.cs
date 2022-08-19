using System.Collections.Generic;
using System.Security.Claims;
using Common.Enums;

namespace Api.Authentication
{
	public class ApiUserIdentity : ClaimsIdentity
	{
		public ApiUserModel UserData { get; set; }

		public ApiUserIdentity(ApiUserModel userData, string authenticationType = "Default") : base(GetUserClaims(userData), authenticationType)
		{
			UserData = userData;
		}

		private static List<Claim> GetUserClaims(ApiUserModel userData)
		{
			if (userData == null || userData.IsBlocked)
			{
				return new List<Claim>();
			}
			var result = new List<Claim>
			{
				new Claim(ClaimTypes.Name, userData.Name),
				new Claim(ClaimTypes.Role, userData.Role.ToString())
			};
			return result;
		}

	}
}

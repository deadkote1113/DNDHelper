using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Api.Enums;
using BL;
using Common;
using Common.Configuration;
using Entities;
using NLog;
using Tools.ConfirmationCodes;

namespace Api.Authentication
{
	public static class TokenProcessor
	{
		private static readonly ConfirmationCodesGenerator generator;

		static TokenProcessor()
		{
			generator = new ConfirmationCodesGenerator(new AesCryptoServiceProvider(), "Frh!zp0IqSz2KxkV", "KOcg!Eo*",
				60L * 60 * 24 * 365, null);
		}

		public static string GetToken(int id, ApiUserRole role, string verificationKey)
		{
			return generator.Generate(Helpers.GenerateRandomString(32, 64), id.ToString(),
				DateTime.Now.Ticks.ToString(), Helpers.GenerateRandomString(32, 64), verificationKey, 
				Helpers.GenerateRandomString(32, 64), role.ToString());
		}

		public static async Task<ApiUserModel> GetUser(string token)
		{
			throw new NotImplementedException("Method not implemented");
			try
			{
				if (string.IsNullOrEmpty(token))
					return null;
				var result = generator.Validate(token);
				if (!result.IsValid || result.ExtractedValues == null || result.ExtractedValues.Count < 7)
					return null;
				if (!Enum.TryParse(result.ExtractedValues[6], out ApiUserRole appUserRole))
					return null;
				if (!int.TryParse(result.ExtractedValues[1], out var appUserId))
					return null;
			}
			catch
			{
				return null;
			}
		}
	}
}
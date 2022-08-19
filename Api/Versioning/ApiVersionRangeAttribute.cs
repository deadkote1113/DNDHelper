using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Api.Versioning
{
	public class ApiVersionRangeAttribute : ApiVersionsBaseAttribute, IApiVersionProvider
	{
		private static readonly List<string> AllApiVersions = new List<string>
		{
			"1.0"
		};

		public static string CurrentApiVersion => AllApiVersions.Last();

		public ApiVersionProviderOptions Options => ApiVersionProviderOptions.None;
		
		public ApiVersionRangeAttribute(string minVersion, string maxVersion = null)
			: base(GetSupportedApiVersions(minVersion, maxVersion))
		{

		}

		private static string[] GetSupportedApiVersions(string minVersion, string maxVersion = null)
		{
			maxVersion ??= AllApiVersions.Last(); 
			var minVersionIndex = AllApiVersions.FindIndex(item => item == minVersion);
			var maxVersionIndex = AllApiVersions.FindIndex(item => item == maxVersion);
			if (minVersionIndex < 0)
			{
				throw new ArgumentException($"Minimal api version {minVersion} not found in the list", nameof(minVersion));
			}
			if (maxVersionIndex < 0)
			{
				throw new ArgumentException($"Minimal api version {maxVersion} not found in the list", nameof(maxVersion));
			}
			return AllApiVersions.Skip(minVersionIndex).Take(maxVersionIndex - minVersionIndex + 1).ToArray();
		}
	}
}

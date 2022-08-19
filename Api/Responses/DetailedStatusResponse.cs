using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Attributes.ApiClientsCodeGenerator;

namespace Api.Responses
{
	[GenericArgumentsNotNull]
	public class DetailedStatusResponse<TStatus> where TStatus : struct
	{
		public TStatus? DetailedStatus { get; set; }

		public string DetailedStatusDescription { get; set; }
	}
}

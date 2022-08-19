using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Common.Attributes;
using Common.Attributes.ApiClientsCodeGenerator;
using Common.Enums;

namespace Api.Requests
{
	[GenericArgumentsNotNull]
	public class RequestWrapper<T>
	{
		public string AccessToken { get; set; }

		public T RequestData { get; set; }
	}
}
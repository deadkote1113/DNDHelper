using Newtonsoft.Json;

namespace Tools.Finances.Ckassa.Responses
{
	public class CreatePaymentError : Error
	{
		[JsonProperty("regPayNum")]
		public string PaymentId { get; set; }

		[JsonProperty("securePageURL")]
		public string PaymentUrl { get; set; }
	}
}

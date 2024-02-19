using Newtonsoft.Json;

namespace Tools.Finances.ApplePay.Models
{
	public class GetPaymentSessionRequest
    {
		public string MerchantIdentifier { get; set; }
		
		[JsonProperty("displayName")]
	    public string ShopDisplayName { get; set; }

	    public string DomainName { get; set; }
	}
}

using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос получения информации о пользователе.
	/// </summary>
	public class GetUserInfoRequest
	{
		/// <summary>
		/// Телефон пользователя.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("login")]
		public string Phone { get; set; }

		public GetUserInfoRequest(string phone)
		{
			Phone = phone;
		}
	}
}

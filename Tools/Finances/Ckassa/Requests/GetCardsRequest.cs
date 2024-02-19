using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на получение списка карт.
	/// </summary>
	public class GetCardsRequest
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(0)]
		public string UserToken { get; set; }

		public GetCardsRequest(string userToken)
		{
			UserToken = userToken;
		}
	}
}

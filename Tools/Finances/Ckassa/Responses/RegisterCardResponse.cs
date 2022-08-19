﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Responses
{
	/// <summary>
	/// Ответ на запрос регистрации карты.
	/// </summary>
	public class RegisterCardResponse
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(0)]
		public string UserToken { get; set; }

		/// <summary>
		/// Идентификатор магазина.
		/// </summary>
		[SignOrder(1)]
		public string ShopToken { get; set; }

		/// <summary>
		/// Идентификатор платежа.
		/// </summary>
		[SignOrder(2)]
		[JsonProperty("regPayNum")]
		public string PaymentId { get; set; }

		/// <summary>
		/// HTTP-метод перехода на страницу оплаты.
		/// </summary>
		[SignOrder(3)]
		[JsonProperty("methodType")]
		public HttpMethod HttpMethod { get; set; }

		/// <summary>
		/// Адрес страницы оплаты.
		/// </summary>
		[SignOrder(4)]
		[JsonProperty("payUrl")]
		public string PaymentUrl { get; set; }
	}
}

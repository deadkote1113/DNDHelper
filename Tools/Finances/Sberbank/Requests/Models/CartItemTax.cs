﻿using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий размер налога на элемент корзины.
	/// </summary>
	public class CartItemTax
	{
		/// <summary>
		/// Тип налога.
		/// </summary>
		public TaxType TaxType { get; set; }

		/// <summary>
		/// Сумма налога.
		/// </summary>
		public decimal? TaxSum { get; set; }
	}
}

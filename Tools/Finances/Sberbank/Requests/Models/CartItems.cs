using System.Collections.Generic;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий корзину товаров.
	/// </summary>
	public class CartItems
	{
		/// <summary>
		/// Товарные позиции корзины.
		/// </summary>
		public List<CartItem> Items { get; set; }
	}
}

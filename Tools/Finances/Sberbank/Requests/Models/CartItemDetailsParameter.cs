namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий дополнительный параметр товарной позиции.
	/// </summary>
	public class CartItemDetailsParameter
	{
		/// <summary>
		/// Имя параметра.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Значение параметра.
		/// </summary>
		public string Value { get; set; }
	}
}

using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Models
{
	/// <summary>
	/// Параметр платежа.
	/// </summary>
	public class PaymentDetail
	{
		/// <summary>
		/// Имя параметра.
		/// </summary>
		[SignOrder(0)]
		public string Name { get; set; }

		/// <summary>
		/// Значение параметра.
		/// </summary>
		[SignOrder(1)]
		public string Value { get; set; }

		public PaymentDetail(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}

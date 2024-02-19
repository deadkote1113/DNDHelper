using System;

namespace Tools.Finances.Ckassa.Attributes
{
	/// <summary>
	/// Порядковый номер свойства, участвующего в генерации подписи для сообщений.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	internal class SignOrderAttribute : Attribute
	{
		public int Order { get; set; }

		public SignOrderAttribute(int order)
		{
			Order = order;
		}
	}
}

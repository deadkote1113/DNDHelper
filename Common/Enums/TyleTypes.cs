using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum TyleTypes
	{
		[Display(Name = "Пустой")]
		Empty = 0,

		[Display(Name = "Крестик")]
		Cross = 1,

		[Display(Name = "Нолик")]
		Circle = 2,
	}
}
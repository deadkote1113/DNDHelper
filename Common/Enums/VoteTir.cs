using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum VoteTir
	{
		[Display(Name = "Бронза")]
		Bronze = 0,

		[Display(Name = "Серебро")]
		Silver = 1,

		[Display(Name = "Золото")]
		Gold = 2,
	}
}
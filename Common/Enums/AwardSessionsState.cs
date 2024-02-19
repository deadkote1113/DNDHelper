using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum AwardSessionsState
	{
		[Display(Name = "Созданно")]
		Created = 0,

		[Display(Name = "В процессе")]
		InProgress = 1,

		[Display(Name = "Завершенно")]
		End = 2,
	}
}
using System.ComponentModel.DataAnnotations;

namespace UI.Other;

public enum AwardItemType
{
	[Display(Name ="Номинация")]
	Nomination = 1,
	[Display(Name = "Событие")]
	Event = 2,
}

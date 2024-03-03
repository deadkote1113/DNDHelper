using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UI.Other;

namespace UI.Areas.Admin.Models.ViewModels;

public class AwardItemViewModel
{
	public ModelWithPicture Item { get; set; }
	public AwardItemType Type { get; set; }
	public int OrderId { get; set; }
	[Required(ErrorMessage = "Укажите значение")]
	[Display(Name = "Завершена")]
	public bool IsCompleted { get; set; }

	public static List<AwardItemViewModel> FromEntities(IEnumerable<NominationModel> nominations, IEnumerable<AwardEventModel> events)
	{
		var result = nominations.Select(FromEntity).Concat(events.Select(FromEntity));

		return result.OrderBy(item => item.OrderId).ToList();
	}

	private static AwardItemViewModel FromEntity(NominationModel nomination)
	{
		return new AwardItemViewModel()
		{
			Item = nomination,
			Type = AwardItemType.Nomination,
			OrderId = nomination.OrderId,
			IsCompleted = nomination.IsCompleted
		};
	}
	private static AwardItemViewModel FromEntity(AwardEventModel @event)
	{
		return new AwardItemViewModel()
		{
			Item = @event,
			Type = AwardItemType.Event,
			OrderId = @event.OrderId,
			IsCompleted = @event.IsCompleted
		};
	}
}

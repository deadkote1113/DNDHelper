using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class AwardEventModel : ModelWithPicture
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "OrderId")]
		public int OrderId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "AwardsId")]
		public int AwardsId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Завершена")]
		public bool IsCompleted { get; set; }

		public static AwardEventModel FromEntity(AwardEvent obj)
		{
			return obj == null ? null : new AwardEventModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.Description,
				OrderId = obj.OrderId,
				AwardsId = obj.AwardsId,
				IsCompleted = obj.IsCompleted,
			};
		}

		public static AwardEvent ToEntity(AwardEventModel obj)
		{
			return obj == null ? null : new AwardEvent(obj.Id, obj.Title, obj.FlavorText, obj.OrderId, obj.AwardsId, obj.IsCompleted);
		}

		public static List<AwardEventModel> FromEntitiesList(IEnumerable<AwardEvent> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<AwardEvent> ToEntitiesList(IEnumerable<AwardEventModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

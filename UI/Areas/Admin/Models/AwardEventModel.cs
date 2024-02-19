using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class AwardEventModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Display(Name = "Title")]
		public string Title { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "OrderId")]
		public int OrderId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "AwardsId")]
		public int AwardsId { get; set; }

		public static AwardEventModel FromEntity(AwardEvent obj)
		{
			return obj == null ? null : new AwardEventModel
			{
				Id = obj.Id,
				Title = obj.Title,
				Description = obj.Description,
				OrderId = obj.OrderId,
				AwardsId = obj.AwardsId,
			};
		}

		public static AwardEvent ToEntity(AwardEventModel obj)
		{
			return obj == null ? null : new AwardEvent(obj.Id, obj.Title, obj.Description, obj.OrderId, obj.AwardsId);
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class AwardModel : ModelWithPicture
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "UserId")]
		public int UserId { get; set; }


		public static AwardModel FromEntity(Award obj)
		{
			return obj == null ? null : new AwardModel
			{
				Id = obj.Id,
				UserId = obj.UserId,
				Title = obj.Title,
				FlavorText = obj.Description,
			};
		}

		public static Award ToEntity(AwardModel obj)
		{
			return obj == null ? null : new Award(obj.Id, obj.UserId, obj.Title, obj.FlavorText);
		}

		public static List<AwardModel> FromEntitiesList(IEnumerable<Award> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Award> ToEntitiesList(IEnumerable<AwardModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

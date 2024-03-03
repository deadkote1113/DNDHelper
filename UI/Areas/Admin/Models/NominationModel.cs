﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class NominationModel : ModelWithPicture
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "AwardsId")]
		public int AwardsId { get; set; }

		[Display(Name = "Порядок")]
		public int OrderId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Завершена")]
		public bool IsCompleted { get; set; }

		public static NominationModel FromEntity(Nomination obj)
		{
			return obj == null ? null : new NominationModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.Description,
				AwardsId = obj.AwardsId,
				OrderId = obj.OrderId,
				IsCompleted = obj.IsCompleted,
			};
		}

		public static Nomination ToEntity(NominationModel obj)
		{
			return obj == null ? null : new Nomination(obj.Id, obj.Title, obj.FlavorText, obj.AwardsId, obj.OrderId, obj.IsCompleted);
		}

		public static List<NominationModel> FromEntitiesList(IEnumerable<Nomination> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Nomination> ToEntitiesList(IEnumerable<NominationModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

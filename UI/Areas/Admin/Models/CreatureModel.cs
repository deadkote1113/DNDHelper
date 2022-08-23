using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class CreatureModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Display(Name = "Описавние")]
		public string FlavorText { get; set; }

		public static CreatureModel FromEntity(Creature obj)
		{
			return obj == null ? null : new CreatureModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,
			};
		}

		public static Creature ToEntity(CreatureModel obj)
		{
			return obj == null ? null : new Creature(obj.Id, obj.Title, obj.FlavorText);
		}

		public static List<CreatureModel> FromEntitiesList(IEnumerable<Creature> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Creature> ToEntitiesList(IEnumerable<CreatureModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

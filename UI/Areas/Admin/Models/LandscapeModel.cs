using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class LandscapeModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Display(Name = "Описавние")]
		public string FlavorText { get; set; }

		public static LandscapeModel FromEntity(Landscape obj)
		{
			return obj == null ? null : new LandscapeModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,
			};
		}

		public static Landscape ToEntity(LandscapeModel obj)
		{
			return obj == null ? null : new Landscape(obj.Id, obj.Title, obj.FlavorText);
		}

		public static List<LandscapeModel> FromEntitiesList(IEnumerable<Landscape> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Landscape> ToEntitiesList(IEnumerable<LandscapeModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

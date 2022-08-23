using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class LocationModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Display(Name = "Описавние")]
		public string FlavorText { get; set; }

		public List<LocationsToContentModel> LocationsToContents { get; set; }

		public static LocationModel FromEntity(Location obj)
		{
			return obj == null ? null : new LocationModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,

				LocationsToContents = LocationsToContentModel.FromEntitiesList(obj.locationsToContents)
			};
		}

		public static Location ToEntity(LocationModel obj)
		{
			return obj == null ? null : new Location(obj.Id, obj.Title, obj.FlavorText);
		}

		public static List<LocationModel> FromEntitiesList(IEnumerable<Location> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Location> ToEntitiesList(IEnumerable<LocationModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

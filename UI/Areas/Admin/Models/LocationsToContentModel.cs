using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class LocationsToContentModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "LocationId")]
		public int LocationId { get; set; }

		[Display(Name = "StructureId")]
		public int? StructureId { get; set; }

		[Display(Name = "LandscapeId")]
		public int? LandscapeId { get; set; }

		public static LocationsToContentModel FromEntity(LocationsToContent obj)
		{
			return obj == null ? null : new LocationsToContentModel
			{
				Id = obj.Id,
				Title = obj.Title,
				LocationId = obj.LocationId,
				StructureId = obj.StructureId,
				LandscapeId = obj.LandscapeId,
			};
		}

		public static LocationsToContent ToEntity(LocationsToContentModel obj)
		{
			return obj == null ? null : new LocationsToContent(obj.Id, obj.Title, obj.LocationId, obj.StructureId,
				obj.LandscapeId);
		}

		public static List<LocationsToContentModel> FromEntitiesList(IEnumerable<LocationsToContent> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<LocationsToContent> ToEntitiesList(IEnumerable<LocationsToContentModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

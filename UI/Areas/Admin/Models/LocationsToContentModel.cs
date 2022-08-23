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
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		public int LocationId { get; set; }

		public int? StructureId { get; set; }

		public int? LandscapeId { get; set; }

		public StructureModel Structure { get; set; }

		public LandscapeModel Landscape { get; set; }

		public static LocationsToContentModel FromEntity(LocationsToContent obj)
		{
			return obj == null ? null : new LocationsToContentModel
			{
				Id = obj.Id,
				Title = obj.Title,
				LocationId = obj.LocationId,
				StructureId = obj.StructureId,
				LandscapeId = obj.LandscapeId,
				Structure = StructureModel.FromEntity(obj.Structure),
				Landscape = LandscapeModel.FromEntity(obj.Landscape),
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

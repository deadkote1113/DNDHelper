using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class LocationsToContent
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int LocationId { get; set; }
		public int? StructureId { get; set; }
		public int? LandscapeId { get; set; }

		public LocationsToContent(int id, string title, int locationId, int? structureId, int? landscapeId)
		{
			Id = id;
			Title = title;
			LocationId = locationId;
			StructureId = structureId;
			LandscapeId = landscapeId;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class StructureModel : ModelWithPicture
	{
		public List<StructuresToItemsOrCreatureModel> StructuresToItemsOrCreatures { get; set; }

		public static StructureModel FromEntity(Structure obj)
		{
			return obj == null ? null : new StructureModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,
				StructuresToItemsOrCreatures = StructuresToItemsOrCreatureModel.FromEntitiesList(obj.StructuresToItemsOrCreatures)
			};
		}

		public static Structure ToEntity(StructureModel obj)
		{
			return obj == null ? null : new Structure(obj.Id, obj.Title, obj.FlavorText);
		}

		public static List<StructureModel> FromEntitiesList(IEnumerable<Structure> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Structure> ToEntitiesList(IEnumerable<StructureModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

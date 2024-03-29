﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class StructuresToItemsOrCreatureModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		public int StructureId { get; set; }

		public int? ItemId { get; set; }

		public int? CreatureId { get; set; }

		public ItemModel Item { get; set; }

		public CreatureModel Creature { get; set; }

		public static StructuresToItemsOrCreatureModel FromEntity(StructuresToItemsOrCreature obj)
		{
			return obj == null ? null : new StructuresToItemsOrCreatureModel
			{
				Id = obj.Id,
				StructureId = obj.StructureId,
				ItemId = obj.ItemId,
				CreatureId = obj.CreatureId,
				Item = ItemModel.FromEntity(obj.Item),
				Creature = CreatureModel.FromEntity(obj.Creature)
			};
		}

		public static StructuresToItemsOrCreature ToEntity(StructuresToItemsOrCreatureModel obj)
		{
			return obj == null ? null : new StructuresToItemsOrCreature(obj.Id, obj.StructureId, obj.ItemId,
				obj.CreatureId);
		}

		public static List<StructuresToItemsOrCreatureModel> FromEntitiesList(IEnumerable<StructuresToItemsOrCreature> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<StructuresToItemsOrCreature> ToEntitiesList(IEnumerable<StructuresToItemsOrCreatureModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

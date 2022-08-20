using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class StructuresToItemsOrCreature
	{
		public int Id { get; set; }
		public int StructureId { get; set; }
		public int? ItemId { get; set; }
		public int? CreatureId { get; set; }

		public StructuresToItemsOrCreature(int id, int structureId, int? itemId, int? creatureId)
		{
			Id = id;
			StructureId = structureId;
			ItemId = itemId;
			CreatureId = creatureId;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class QuestsToItemsOrCreature
	{
		public int Id { get; set; }
		public int QuestId { get; set; }
		public int? ItemId { get; set; }
		public int? CreatureId { get; set; }

		public List<Quest> Quests { get; set; }

		public QuestsToItemsOrCreature(int id, int questId, int? itemId, int? creatureId)
		{
			Id = id;
			QuestId = questId;
			ItemId = itemId;
			CreatureId = creatureId;
		}
	}
}

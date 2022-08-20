using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Item
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string FlavorText { get; set; }
		public int? CreaturesId { get; set; }
		public int? StructuresId { get; set; }

		//public List<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; set; }

		public Item(int id, string title, string flavorText, int? creaturesId, int? structuresId)
		{
			Id = id;
			Title = title;
			FlavorText = flavorText;
			CreaturesId = creaturesId;
			StructuresId = structuresId;
		}
	}
}

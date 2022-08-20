using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Quest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string FlavorText { get; set; }
		public bool IsComplited { get; set; }
		public int? NextQuestId { get; set; }

		public Quest(int id, string title, string flavorText, bool isComplited, int? nextQuestId)
		{
			Id = id;
			Title = title;
			FlavorText = flavorText;
			IsComplited = isComplited;
			NextQuestId = nextQuestId;
		}
	}
}

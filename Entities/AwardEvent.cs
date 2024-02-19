using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class AwardEvent
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int OrderId { get; set; }
		public int AwardsId { get; set; }

		public AwardEvent(int id, string title, string description, int orderId, int awardsId)
		{
			Id = id;
			Title = title;
			Description = description;
			OrderId = orderId;
			AwardsId = awardsId;
		}
	}
}

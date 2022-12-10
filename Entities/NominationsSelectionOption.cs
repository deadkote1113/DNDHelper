using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class NominationsSelectionOption
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int NominationId { get; set; }

		public NominationsSelectionOption(int id, int? userId, string title, string description, int nominationId)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Description = description;
			NominationId = nominationId;
		}
	}
}

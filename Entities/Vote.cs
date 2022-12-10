using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Vote
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public int NominationsSelectionOptionsId { get; set; }

		public Vote(int id, int? userId, int nominationsSelectionOptionsId)
		{
			Id = id;
			UserId = userId;
			NominationsSelectionOptionsId = nominationsSelectionOptionsId;
		}
	}
}

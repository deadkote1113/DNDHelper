using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Award
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public Award(int id, int userId, string title, string description)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Description = description;
		}
	}
}

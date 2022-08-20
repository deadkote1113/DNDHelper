using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Creature
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string FlavorText { get; set; }

		public Creature(int id, string title, string flavorText)
		{
			Id = id;
			Title = title;
			FlavorText = flavorText;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Picture
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string PicturePath { get; set; }

		public Picture(int id, string title, string picturePath)
		{
			Id = id;
			Title = title;
			PicturePath = picturePath;
		}
	}
}

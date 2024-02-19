using Common.Enums;

namespace Entities
{
	public class Picture
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public PictureType Type { get; set; }

		public Picture(int id, string title, string picturePath, PictureType type)
		{
			Id = id;
			Title = title;
			Link = picturePath;
			Type = type;
		}
	}
}

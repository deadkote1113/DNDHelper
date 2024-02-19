using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class PictureModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Укажите значение путь")]
		[Display(Name = "Путь")]
		public string PicturePath { get; set; }

		public static PictureModel FromEntity(Picture obj)
		{
			return obj == null ? null : new PictureModel
			{
				Id = obj.Id,
				Title = obj.Title,
				PicturePath = obj.Link,
			};
		}

		public static Picture ToEntity(PictureModel obj)
		{
			return obj == null ? null : new Picture(obj.Id, obj.Title, obj.PicturePath);
		}

		public static List<PictureModel> FromEntitiesList(IEnumerable<Picture> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Picture> ToEntitiesList(IEnumerable<PictureModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

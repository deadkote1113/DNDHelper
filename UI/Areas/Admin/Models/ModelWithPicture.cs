using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Models
{
	public class ModelWithPicture
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Display(Name = "Описание")]
		public string FlavorText { get; set; }
	}
}

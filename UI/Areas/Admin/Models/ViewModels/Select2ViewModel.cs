namespace UI.Areas.Admin.Models.ViewModels
{
	public class Select2ViewModel
	{
		public string id { get; set; }
		public string text { get; set; }

		public Select2ViewModel(string id, string text)
		{
			this.id = id;
			this.text = text;
		}
	}
}
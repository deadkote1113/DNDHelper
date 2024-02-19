namespace UI.Areas.Admin.Models.ViewModels
{
	public class PictureViewModel
	{
		public PictureViewModel(PictureModel picture, int pictureLinkId)
		{
			Picture = picture;
			PictureLinkId = pictureLinkId;
		}

		public PictureModel Picture { get; set; }

		public int PictureLinkId { get; set; }
	}
}
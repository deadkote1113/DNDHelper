namespace Entities
{
	public class PicturesToOther
	{
		public int Id { get; set; }
		public int? PictureId { get; set; }
		public int? AwardId { get; set; }
		public int? NominationId { get; set; }
		public int? NominationsSelectionOptionId { get; set; }
		public int? AwardEventId { get; set; }

		public Picture Picture { get; set; }

		public PicturesToOther() { }

		public PicturesToOther(int id, int? pictureId, int? awardId, int? nominationId, int? nominationsSelectionOptionId, int? awardEventId)
		{
			Id = id;
			PictureId = pictureId;
			AwardId = awardId;
			NominationId = nominationId;
			NominationsSelectionOptionId = nominationsSelectionOptionId;
			AwardEventId = awardEventId;
		}
	}
}

namespace Common.Search
{
	public class PicturesToOtherSearchParams : BaseSearchParams
	{
		public int? ItemId { get; set; }
		public int? CreatureId { get; set; }
		public int? StructureId { get; set; }
		public int? AwardId { get; set; }
		public int? NominationId { get; set; }
		public int? NominationsSelectionOptionId { get; set; }

		public PicturesToOtherSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

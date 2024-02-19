using Common.Enums;
using Common.Search;

namespace UI.Areas.Admin.Models.ViewModels.FilterModels
{
	public class InnerPictureFilterModel
	{
		public PictureType Type { get; set; }
		public int Id { get; set; }
		public int? Counts { get; set; }
		public string ViewName { get; set; }

		public static PicturesToOtherSearchParams ConvertToSearchParams(InnerPictureFilterModel filter)
		{
			var result = new PicturesToOtherSearchParams();
			result.ObjectsCount = filter.Counts;
			switch (filter.Type)
			{
				case PictureType.Award:
					{
						result.AwardId = filter.Id;
						break;
					}
				case PictureType.Nomination:
					{
						result.NominationId = filter.Id;
						break;
					}
				case PictureType.NominationSelectionOption:
					{
						result.NominationsSelectionOptionId = filter.Id;
						break;
					}
			}
			return result;
		}
	}
}

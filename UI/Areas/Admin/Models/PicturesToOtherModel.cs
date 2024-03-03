using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class PicturesToOtherModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		public int Id { get; set; }

		public int? PictureId { get; set; }

		public int? AwardId { get; set; }

		public int? NominationId { get; set; }

		public int? NominationsSelectionOptionId { get; set; }

		public int? AwardEventId { get; set; }

		public static PicturesToOtherModel FromEntity(PicturesToOther obj)
		{
			return obj == null ? null : new PicturesToOtherModel
			{
				Id = obj.Id,
				PictureId = obj.PictureId,
				AwardId = obj.AwardId,
				NominationId = obj.NominationId,
				NominationsSelectionOptionId = obj.NominationsSelectionOptionId,
				AwardEventId = obj.AwardEventId
			};
		}

		public static PicturesToOther ToEntity(PicturesToOtherModel obj)
		{
			return obj == null ? null : new PicturesToOther(obj.Id, obj.PictureId, obj.AwardId, obj.NominationId, obj.NominationsSelectionOptionId, obj.AwardEventId);
		}

		public static List<PicturesToOtherModel> FromEntitiesList(IEnumerable<PicturesToOther> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<PicturesToOther> ToEntitiesList(IEnumerable<PicturesToOtherModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

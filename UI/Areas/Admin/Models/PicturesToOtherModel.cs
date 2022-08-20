using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class PicturesToOtherModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Display(Name = "PictureId")]
		public int? PictureId { get; set; }

		[Display(Name = "ItemId")]
		public int? ItemId { get; set; }

		[Display(Name = "CreatureId")]
		public int? CreatureId { get; set; }

		[Display(Name = "StructureId")]
		public int? StructureId { get; set; }

		public static PicturesToOtherModel FromEntity(PicturesToOther obj)
		{
			return obj == null ? null : new PicturesToOtherModel
			{
				Id = obj.Id,
				PictureId = obj.PictureId,
				ItemId = obj.ItemId,
				CreatureId = obj.CreatureId,
				StructureId = obj.StructureId,
			};
		}

		public static PicturesToOther ToEntity(PicturesToOtherModel obj)
		{
			return obj == null ? null : new PicturesToOther(obj.Id, obj.PictureId, obj.ItemId, obj.CreatureId,
				obj.StructureId);
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

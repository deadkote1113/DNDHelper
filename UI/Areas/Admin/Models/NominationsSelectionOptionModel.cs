using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class NominationsSelectionOptionModel : ModelWithPicture
	{
		[Display(Name = "UserId")]
		public int? UserId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		public int NominationId { get; set; }

		public static NominationsSelectionOptionModel FromEntity(NominationsSelectionOption obj)
		{
			return obj == null ? null : new NominationsSelectionOptionModel
			{
				Id = obj.Id,
				UserId = obj.UserId,
				Title = obj.Title,
				FlavorText = obj.Description,
				NominationId = obj.NominationId,
			};
		}

		public static NominationsSelectionOption ToEntity(NominationsSelectionOptionModel obj)
		{
			return obj == null ? null : new NominationsSelectionOption(obj.Id, obj.UserId, obj.Title, obj.FlavorText,
				obj.NominationId);
		}

		public static List<NominationsSelectionOptionModel> FromEntitiesList(IEnumerable<NominationsSelectionOption> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<NominationsSelectionOption> ToEntitiesList(IEnumerable<NominationsSelectionOptionModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

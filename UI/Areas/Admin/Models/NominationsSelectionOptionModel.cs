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

		[Display(Name = "Зачтёт")]
		public int ReaderId { get; set; }

		public string ReaderName { get; set; }

		public static NominationsSelectionOptionModel FromEntity(NominationsSelectionOption obj)
		{
			return obj == null ? null : new NominationsSelectionOptionModel
			{
				Id = obj.Id,
				UserId = obj.UserId,
				Title = obj.Title,
				FlavorText = obj.Description,
				NominationId = obj.NominationId,
				ReaderId = obj.ReaderId,

				ReaderName = obj.Reader.Name,
			};
		}

		public static NominationsSelectionOption ToEntity(NominationsSelectionOptionModel obj)
		{
			return obj == null ? null : new NominationsSelectionOption(obj.Id, obj.UserId, obj.Title, obj.FlavorText,
				obj.NominationId, obj.ReaderId);
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

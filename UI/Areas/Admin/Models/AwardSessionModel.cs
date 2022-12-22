using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class AwardSessionModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "UserId")]
		public int UserId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "ConnectionCode")]
		public string ConnectionCode { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "State")]
		public AwardSessionsState State { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "NominationPassed")]
		public int NominationPassed { get; set; }

		public int AwardId { get; set; }

		public static AwardSessionModel FromEntity(AwardSession obj)
		{
			return obj == null ? null : new AwardSessionModel
			{
				Id = obj.Id,
				UserId = obj.UserId,
				ConnectionCode = obj.ConnectionCode,
				State = obj.State,
				NominationPassed = obj.NominationPassed,
				AwardId = obj.AwardId,
			};
		}

		public static AwardSession ToEntity(AwardSessionModel obj)
		{
			return obj == null ? null : new AwardSession(obj.Id, obj.UserId, obj.ConnectionCode, obj.State,
				obj.NominationPassed, obj.AwardId);
		}

		public static List<AwardSessionModel> FromEntitiesList(IEnumerable<AwardSession> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<AwardSession> ToEntitiesList(IEnumerable<AwardSessionModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

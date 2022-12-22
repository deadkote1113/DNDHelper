﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class VoteModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Display(Name = "UserId")]
		public int? UserId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "NominationsSelectionOptionsId")]
		public int NominationsSelectionOptionsId { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IsCanseld")]
		public bool IsCanseld { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "TelegramUserName")]
		public string TelegramUserName { get; set; }

		public static VoteModel FromEntity(Vote obj)
		{
			return obj == null ? null : new VoteModel
			{
				Id = obj.Id,
				UserId = obj.UserId,
				NominationsSelectionOptionsId = obj.NominationsSelectionOptionsId,
				IsCanseld = obj.IsCanseld,
				TelegramUserName = obj.TelegramUserName,
			};
		}

		public static Vote ToEntity(VoteModel obj)
		{
			return obj == null ? null : new Vote(obj.Id, obj.UserId, obj.NominationsSelectionOptionsId, obj.IsCanseld,
				obj.TelegramUserName);
		}

		public static List<VoteModel> FromEntitiesList(IEnumerable<Vote> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Vote> ToEntitiesList(IEnumerable<VoteModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

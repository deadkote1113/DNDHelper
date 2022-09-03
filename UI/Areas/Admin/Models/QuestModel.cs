using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class QuestModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение название")]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Display(Name = "Описавние")]
		public string FlavorText { get; set; }

		[Required(ErrorMessage = "укажите Завершён ли квест")]
		[Display(Name = "Завершён")]
		public bool IsComplited { get; set; }

		public int? NextQuestId { get; set; }

		public QuestModel NextQuests { get; set; }

		public static QuestModel FromEntity(Quest obj)
		{
			return obj == null ? null : new QuestModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,
				IsComplited = obj.IsComplited,
				NextQuestId = obj.NextQuestId,
				NextQuests = QuestModel.FromEntity(obj.NextQuests)
			};
		}

		public static Quest ToEntity(QuestModel obj)
		{
			return obj == null ? null : new Quest(obj.Id, obj.Title, obj.FlavorText, obj.IsComplited, obj.NextQuestId);
		}

		public static List<QuestModel> FromEntitiesList(IEnumerable<Quest> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Quest> ToEntitiesList(IEnumerable<QuestModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

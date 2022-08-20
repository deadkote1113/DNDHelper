using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class QuestsToItemsOrCreatureModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "QuestId")]
		public int QuestId { get; set; }

		[Display(Name = "ItemId")]
		public int? ItemId { get; set; }

		[Display(Name = "CreatureId")]
		public int? CreatureId { get; set; }

		public List<QuestModel> Quests { get; set; }

		public static QuestsToItemsOrCreatureModel FromEntity(QuestsToItemsOrCreature obj)
		{
			return obj == null ? null : new QuestsToItemsOrCreatureModel
			{
				Id = obj.Id,
				QuestId = obj.QuestId,
				ItemId = obj.ItemId,
				CreatureId = obj.CreatureId,
				Quests = QuestModel.FromEntitiesList(obj.Quests),
			};
		}

		public static QuestsToItemsOrCreature ToEntity(QuestsToItemsOrCreatureModel obj)
		{
			return obj == null ? null : new QuestsToItemsOrCreature(obj.Id, obj.QuestId, obj.ItemId, obj.CreatureId);
		}

		public static List<QuestsToItemsOrCreatureModel> FromEntitiesList(IEnumerable<QuestsToItemsOrCreature> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<QuestsToItemsOrCreature> ToEntitiesList(IEnumerable<QuestsToItemsOrCreatureModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

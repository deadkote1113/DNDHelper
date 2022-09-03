using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class ItemModel : ModelWithPicture
	{
		public int? CreaturesId { get; set; }

		public int? StructuresId { get; set; }

		public static ItemModel FromEntity(Item obj)
		{
			return obj == null ? null : new ItemModel
			{
				Id = obj.Id,
				Title = obj.Title,
				FlavorText = obj.FlavorText,
				CreaturesId = obj.CreaturesId,
				StructuresId = obj.StructuresId,
			};
		}

		public static Item ToEntity(ItemModel obj)
		{
			return obj == null ? null : new Item(obj.Id, obj.Title, obj.FlavorText, obj.CreaturesId, obj.StructuresId);
		}

		public static List<ItemModel> FromEntitiesList(IEnumerable<Item> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Item> ToEntitiesList(IEnumerable<ItemModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class ItemModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Display(Name = "FlavorText")]
		public string FlavorText { get; set; }

		[Display(Name = "CreaturesId")]
		public int? CreaturesId { get; set; }

		[Display(Name = "StructuresId")]
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

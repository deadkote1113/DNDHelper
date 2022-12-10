using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Common.Search;
using Entities;

namespace UI.Areas.Admin.Models.ViewModels.FilterModels
{
	public class InnerPictureFilterModel
	{
		public PictureType Type { get; set; }
		public int Id { get; set; }
		public int? Counts { get; set; }
		public string ViewName { get; set; }

		public static PicturesToOtherSearchParams ConvertToSearchParams(InnerPictureFilterModel filter)
		{
			var result = new PicturesToOtherSearchParams();
			result.ObjectsCount = filter.Counts;
			switch (filter.Type)
			{
				case PictureType.Item:
					{
						result.ItemId = filter.Id;
						break;
					}
				case PictureType.Create:
					{
						result.CreatureId = filter.Id;
						break;
					}
				case PictureType.Structure:
					{
						result.StructureId = filter.Id;
						break;
					}
				case PictureType.Award:
					{
						result.AwardId = filter.Id;
						break;
					}
				case PictureType.Nomination:
					{
						result.NominationId = filter.Id;
						break;
					}
				case PictureType.NominationSelectionOption:
					{
						result.NominationsSelectionOptionId = filter.Id;
						break;
					}
			}
			return result;
		}
	}
}

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

		public static PicturesToOtherSearchParams ConvertToSearchParams(InnerPictureFilterModel filter)
		{
			var result = new PicturesToOtherSearchParams();

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
			}
			return result;
		}
	}
}

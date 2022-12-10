using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class PicturesToOther
	{
		public int Id { get; set; }
		public int? PictureId { get; set; }
		public int? ItemId { get; set; }
		public int? CreatureId { get; set; }
		public int? StructureId { get; set; }
		public int? AwardId { get; set; }
		public int? NominationId { get; set; }
		public int? NominationsSelectionOptionId { get; set; }

		public Picture Picture { get; set; }

		public PicturesToOther() { }

		public PicturesToOther(int id, int? pictureId, int? itemId, int? creatureId, int? structureId,
			int? awardId, int? nominationId, int? nominationsSelectionOptionId)
		{
			Id = id;
			PictureId = pictureId;
			ItemId = itemId;
			CreatureId = creatureId;
			StructureId = structureId;
			AwardId = awardId;
			NominationId = nominationId;
			NominationsSelectionOptionId = nominationsSelectionOptionId;
		}
	}
}

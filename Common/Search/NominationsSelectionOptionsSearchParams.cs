using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class NominationsSelectionOptionsSearchParams : BaseSearchParams
	{
		public int? NominationId { get; set; }

		public NominationsSelectionOptionsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

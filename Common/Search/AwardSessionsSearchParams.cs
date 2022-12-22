using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class AwardSessionsSearchParams : BaseSearchParams
	{
		public AwardSessionsState? State { get; set; }

		public AwardSessionsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

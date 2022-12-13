using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class VotesSearchParams : BaseSearchParams
	{
		public int? NominationId { get; set; }
		public VotesSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

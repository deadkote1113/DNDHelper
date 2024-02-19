﻿namespace Common.Search
{
	public class AwardsSearchParams : BaseSearchParams
	{
		public string SearchQuery { get; set; }

		public AwardsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

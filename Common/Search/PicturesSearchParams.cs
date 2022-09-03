using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class PicturesSearchParams : BaseSearchParams
	{
		public string SearchQuery { get; set; }

		public PicturesSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

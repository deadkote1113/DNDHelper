using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class LocationsSearchParams : BaseSearchParams
	{
		public LocationsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

﻿using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class NominationsSearchParams : BaseSearchParams
	{
		public int? AwardId { get; set; }

		public NominationsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

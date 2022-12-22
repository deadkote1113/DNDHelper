using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class AwardSession
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string ConnectionCode { get; set; }
		public int State { get; set; }
		public int NominationPassed { get; set; }

		public AwardSession(int id, int userId, string connectionCode, int state, int nominationPassed)
		{
			Id = id;
			UserId = userId;
			ConnectionCode = connectionCode;
			State = state;
			NominationPassed = nominationPassed;
		}
	}
}

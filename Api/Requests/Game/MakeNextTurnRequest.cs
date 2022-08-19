using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Models;
namespace Api.Requests
{
	public class MakeNextTurnRequest
	{
		public List<FieldModel> Fields { get; set; }
		public int GameId { get; set; }

	}
}
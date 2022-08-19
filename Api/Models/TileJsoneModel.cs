using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;
using User = Entities.User;

namespace Api.Models
{
	public class TileModel
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Type { get; set; }
	}
}

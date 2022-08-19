using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;
using User = Entities.User;

namespace Api.Models
{
	public class FieldModel
	{
		public TileModel[] Tiles { get; set; }
		public int GameId { get; set; }
	}
}

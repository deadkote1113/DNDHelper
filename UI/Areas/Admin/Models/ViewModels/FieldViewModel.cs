using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;
using User = Entities.User;

namespace UI.Areas.Admin.Models.ViewModels
{
	public class FieldViewModel
	{
		public List<TileViewModel> Tiles { get; set; }
		public int GameId { get; set; }
	}
}

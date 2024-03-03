using System.Collections.Generic;

namespace UI.Areas.Admin.Models.ViewModels
{
	public class SeeNominationsViewModel
	{
		public SeeNominationsViewModel(AwardItemViewModel nimination, List<NominationsSelectionOptionModel>? options)
		{
			Nimination = nimination;
			this.Options = options;
		}

		public AwardItemViewModel Nimination { get; set; }
		public List<NominationsSelectionOptionModel>? Options { get; set; }
	}
}
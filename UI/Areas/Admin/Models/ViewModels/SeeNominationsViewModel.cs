using System.Collections.Generic;

namespace UI.Areas.Admin.Models.ViewModels
{
	public class SeeNominationsViewModel
	{
		public SeeNominationsViewModel(NominationModel nimination, List<NominationsSelectionOptionModel> options)
		{
			Nimination = nimination;
			this.options = options;
		}

		public NominationModel Nimination { get; set; }
		public List<NominationsSelectionOptionModel> options { get; set; }
	}
}
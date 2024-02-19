using System.Collections.Generic;

namespace UI.Areas.Admin.Models.ViewModels
{
	public class VoteViewModel
	{
		public VoteViewModel(NominationsSelectionOptionModel option, List<VoteModel> vote)
		{
			Option = option;
			Vote = vote;
		}

		public NominationsSelectionOptionModel Option { get; set; }
		public List<VoteModel> Vote { get; set; }
	}
}

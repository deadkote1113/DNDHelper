using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Nomination
    {
        public Nomination()
        {
            NominationsSelectionOptions = new HashSet<NominationsSelectionOption>();
            PicturesToOthers = new HashSet<PicturesToOther>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AwardsId { get; set; }

        public virtual Award Awards { get; set; }
        public virtual ICollection<NominationsSelectionOption> NominationsSelectionOptions { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
    }
}

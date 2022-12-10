using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class NominationsSelectionOption
    {
        public NominationsSelectionOption()
        {
            PicturesToOthers = new HashSet<PicturesToOther>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NominationId { get; set; }

        public virtual Nomination Nomination { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}

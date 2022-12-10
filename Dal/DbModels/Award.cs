using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Award
    {
        public Award()
        {
            Nominations = new HashSet<Nomination>();
            PicturesToOthers = new HashSet<PicturesToOther>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Nomination> Nominations { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
    }
}

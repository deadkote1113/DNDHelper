using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Picture
    {
        public Picture()
        {
            PicturesToOthers = new HashSet<PicturesToOther>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
    }
}

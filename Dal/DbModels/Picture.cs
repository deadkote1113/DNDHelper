using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PicturePath { get; set; }
        public int? PicturesToOtherId { get; set; }

        public virtual PicturesToOther PicturesToOther { get; set; }
    }
}

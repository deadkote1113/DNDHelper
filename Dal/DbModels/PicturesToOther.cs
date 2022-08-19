using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class PicturesToOther
    {
        public PicturesToOther()
        {
            Pictures = new HashSet<Picture>();
        }

        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int? CreatureId { get; set; }
        public int? StructureId { get; set; }

        public virtual Creature Creature { get; set; }
        public virtual Item Item { get; set; }
        public virtual Structure Structure { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}

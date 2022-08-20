using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Item
    {
        public Item()
        {
            PicturesToOthers = new HashSet<PicturesToOther>();
            QuestsToItemsOrCreatures = new HashSet<QuestsToItemsOrCreature>();
            StructuresToItemsOrCreatures = new HashSet<StructuresToItemsOrCreature>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
        public int? CreaturesId { get; set; }
        public int? StructuresId { get; set; }

        public virtual Creature Creatures { get; set; }
        public virtual Structure Structures { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
        public virtual ICollection<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; set; }
        public virtual ICollection<StructuresToItemsOrCreature> StructuresToItemsOrCreatures { get; set; }
    }
}

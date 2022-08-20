using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Structure
    {
        public Structure()
        {
            Items = new HashSet<Item>();
            LocationsToContents = new HashSet<LocationsToContent>();
            PicturesToOthers = new HashSet<PicturesToOther>();
            StructuresToItemsOrCreatures = new HashSet<StructuresToItemsOrCreature>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<LocationsToContent> LocationsToContents { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
        public virtual ICollection<StructuresToItemsOrCreature> StructuresToItemsOrCreatures { get; set; }
    }
}

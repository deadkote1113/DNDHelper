using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Structure
    {
        public Structure()
        {
            Creatures = new HashSet<Creature>();
            Items = new HashSet<Item>();
            PicturesToOthers = new HashSet<PicturesToOther>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
        public int? LocationsToContentsId { get; set; }

        public virtual LocationsToContent LocationsToContents { get; set; }
        public virtual ICollection<Creature> Creatures { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<PicturesToOther> PicturesToOthers { get; set; }
    }
}

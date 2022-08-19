using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class LocationsToContent
    {
        public LocationsToContent()
        {
            Landscapes = new HashSet<Landscape>();
            Structures = new HashSet<Structure>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Landscape> Landscapes { get; set; }
        public virtual ICollection<Structure> Structures { get; set; }
    }
}

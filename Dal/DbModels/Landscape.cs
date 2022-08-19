using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Landscape
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
        public int? LocationsToContentsId { get; set; }

        public virtual LocationsToContent LocationsToContents { get; set; }
    }
}

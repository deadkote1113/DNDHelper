﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Location
    {
        public Location()
        {
            LocationsToContents = new HashSet<LocationsToContent>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }

        public virtual ICollection<LocationsToContent> LocationsToContents { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Location
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FlavorText { get; set; }

    public virtual ICollection<LocationsToContent> LocationsToContents { get; } = new List<LocationsToContent>();
}

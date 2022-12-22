using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class LocationsToContent
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int LocationId { get; set; }

    public int? StructureId { get; set; }

    public int? LandscapeId { get; set; }

    public virtual Landscape Landscape { get; set; }

    public virtual Location Location { get; set; }

    public virtual Structure Structure { get; set; }
}

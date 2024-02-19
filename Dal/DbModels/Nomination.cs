﻿using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Nomination
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int OrderId { get; set; }

    public int AwardsId { get; set; }

    public int ReaderId { get; set; }

    public virtual Award Awards { get; set; }

    public virtual ICollection<NominationsSelectionOption> NominationsSelectionOptions { get; } = new List<NominationsSelectionOption>();

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();

    public virtual Reader Reader { get; set; }
}

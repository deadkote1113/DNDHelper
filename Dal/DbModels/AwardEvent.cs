using System.Collections.Generic;

namespace Dal.DbModels;

public partial class AwardEvent
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int OrderId { get; set; }

    public int AwardsId { get; set; }

    public virtual Award Awards { get; set; }

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();
}

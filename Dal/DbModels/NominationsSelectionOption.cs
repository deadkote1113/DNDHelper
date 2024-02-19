using System.Collections.Generic;

namespace Dal.DbModels;

public partial class NominationsSelectionOption
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int NominationId { get; set; }

    public int ReaderId { get; set; }

    public virtual Nomination Nomination { get; set; }

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();

    public virtual Reader Reader { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Vote> Votes { get; } = new List<Vote>();
}

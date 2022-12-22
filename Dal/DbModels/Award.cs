using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Award
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public virtual ICollection<AwardSession> AwardSessions { get; } = new List<AwardSession>();

    public virtual ICollection<Nomination> Nominations { get; } = new List<Nomination>();

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();

    public virtual User User { get; set; }
}

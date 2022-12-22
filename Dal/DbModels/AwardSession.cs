using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class AwardSession
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ConnectionCode { get; set; }

    public int State { get; set; }

    public int NominationPassed { get; set; }

    public virtual User User { get; set; }
}

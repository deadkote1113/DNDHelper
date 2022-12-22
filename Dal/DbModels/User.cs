using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<AwardSession> AwardSessions { get; } = new List<AwardSession>();

    public virtual ICollection<Award> Awards { get; } = new List<Award>();

    public virtual ICollection<NominationsSelectionOption> NominationsSelectionOptions { get; } = new List<NominationsSelectionOption>();

    public virtual ICollection<Vote> Votes { get; } = new List<Vote>();
}

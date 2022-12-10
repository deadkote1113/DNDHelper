using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class User
    {
        public User()
        {
            Awards = new HashSet<Award>();
            NominationsSelectionOptions = new HashSet<NominationsSelectionOption>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Award> Awards { get; set; }
        public virtual ICollection<NominationsSelectionOption> NominationsSelectionOptions { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}

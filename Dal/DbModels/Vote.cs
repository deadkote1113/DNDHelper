using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Vote
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int NominationsSelectionOptionsId { get; set; }

        public virtual NominationsSelectionOption NominationsSelectionOptions { get; set; }
        public virtual User User { get; set; }
    }
}

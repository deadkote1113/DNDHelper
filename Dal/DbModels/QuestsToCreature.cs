using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class QuestsToCreature
    {
        public QuestsToCreature()
        {
            Quests = new HashSet<Quest>();
        }

        public int Id { get; set; }
        public int QuestId { get; set; }

        public virtual Quest Quest { get; set; }
        public virtual ICollection<Quest> Quests { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class Quest
    {
        public Quest()
        {
            InverseNextQuest = new HashSet<Quest>();
            QuestsToItemsOrCreatures = new HashSet<QuestsToItemsOrCreature>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
        public bool IsComplited { get; set; }
        public int? NextQuestId { get; set; }

        public virtual Quest NextQuest { get; set; }
        public virtual ICollection<Quest> InverseNextQuest { get; set; }
        public virtual ICollection<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; set; }
    }
}

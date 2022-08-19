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
            QuestsToCreatures = new HashSet<QuestsToCreature>();
            QuestsToItemsNavigation = new HashSet<QuestsToItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FlavorText { get; set; }
        public bool IsComplited { get; set; }
        public int? NextQuestId { get; set; }
        public int? QuestsToCreatureId { get; set; }
        public int? QuestsToItemsId { get; set; }

        public virtual Quest NextQuest { get; set; }
        public virtual QuestsToCreature QuestsToCreature { get; set; }
        public virtual QuestsToItem QuestsToItems { get; set; }
        public virtual ICollection<Quest> InverseNextQuest { get; set; }
        public virtual ICollection<QuestsToCreature> QuestsToCreatures { get; set; }
        public virtual ICollection<QuestsToItem> QuestsToItemsNavigation { get; set; }
    }
}

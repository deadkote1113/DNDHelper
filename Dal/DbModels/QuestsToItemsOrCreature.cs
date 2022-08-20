using System;
using System.Collections.Generic;

#nullable disable

namespace Dal.DbModels
{
    public partial class QuestsToItemsOrCreature
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int ItemId { get; set; }
        public int CreatureId { get; set; }

        public virtual Creature Creature { get; set; }
        public virtual Item Item { get; set; }
        public virtual Quest Quest { get; set; }
    }
}

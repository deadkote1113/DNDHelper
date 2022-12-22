using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Quest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FlavorText { get; set; }

    public bool IsComplited { get; set; }

    public int? NextQuestId { get; set; }

    public virtual ICollection<Quest> InverseNextQuest { get; } = new List<Quest>();

    public virtual Quest NextQuest { get; set; }

    public virtual ICollection<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; } = new List<QuestsToItemsOrCreature>();
}

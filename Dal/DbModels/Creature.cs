using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Creature
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FlavorText { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();

    public virtual ICollection<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; } = new List<QuestsToItemsOrCreature>();

    public virtual ICollection<StructuresToItemsOrCreature> StructuresToItemsOrCreatures { get; } = new List<StructuresToItemsOrCreature>();
}

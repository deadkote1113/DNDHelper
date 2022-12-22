using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Item
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FlavorText { get; set; }

    public int? CreaturesId { get; set; }

    public int? StructuresId { get; set; }

    public virtual Creature Creatures { get; set; }

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();

    public virtual ICollection<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; } = new List<QuestsToItemsOrCreature>();

    public virtual Structure Structures { get; set; }

    public virtual ICollection<StructuresToItemsOrCreature> StructuresToItemsOrCreatures { get; } = new List<StructuresToItemsOrCreature>();
}

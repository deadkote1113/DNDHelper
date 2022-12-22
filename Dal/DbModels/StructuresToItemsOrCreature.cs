using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class StructuresToItemsOrCreature
{
    public int Id { get; set; }

    public int StructureId { get; set; }

    public int? ItemId { get; set; }

    public int? CreatureId { get; set; }

    public virtual Creature Creature { get; set; }

    public virtual Item Item { get; set; }

    public virtual Structure Structure { get; set; }
}

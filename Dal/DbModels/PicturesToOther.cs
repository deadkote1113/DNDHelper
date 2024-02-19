namespace Dal.DbModels;

public partial class PicturesToOther
{
    public int Id { get; set; }

    public int? PictureId { get; set; }

    public int? ItemId { get; set; }

    public int? CreatureId { get; set; }

    public int? StructureId { get; set; }

    public int? AwardId { get; set; }

    public int? NominationId { get; set; }

    public int? NominationsSelectionOptionId { get; set; }

    public virtual Award Award { get; set; }

    public virtual Nomination Nomination { get; set; }

    public virtual NominationsSelectionOption NominationsSelectionOption { get; set; }

    public virtual Picture Picture { get; set; }
}

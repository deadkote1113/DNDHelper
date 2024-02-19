using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Picture
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string PicturePath { get; set; }

    public virtual ICollection<PicturesToOther> PicturesToOthers { get; } = new List<PicturesToOther>();
}

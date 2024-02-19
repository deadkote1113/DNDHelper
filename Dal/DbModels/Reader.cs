using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Reader
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Nomination> Nominations { get; } = new List<Nomination>();
}

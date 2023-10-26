using System;
using System.Collections.Generic;

namespace Note_Taker.DbModels;

public partial class Note
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Process { get; set; }

    public virtual Process ProcessNavigation { get; set; } = null!;
}

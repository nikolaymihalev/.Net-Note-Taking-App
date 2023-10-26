using System;
using System.Collections.Generic;

namespace Note_Taker.DbModels;

public partial class Process
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}

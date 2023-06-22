using System;
using System.Collections.Generic;

namespace DL;

public partial class Area
{
    public int IdArea { get; set; }

    public string? NombreArea { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

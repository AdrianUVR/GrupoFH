using System;
using System.Collections.Generic;

namespace DL;

public partial class Error
{
    public int IdError { get; set; }

    public string? DescripcionE { get; set; }

    public string? Paso1 { get; set; }

    public string? Paso2 { get; set; }

    public string? Paso3 { get; set; }

    public int? IdArea { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public string NombreArea { get; set; }
}

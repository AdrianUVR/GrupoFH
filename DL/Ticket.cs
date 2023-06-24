using System;
using System.Collections.Generic;

namespace DL;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public int? AsignadoA { get; set; }

    public int? CerradoPor { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public int? IdArea { get; set; }

    public bool? Status { get; set; }

    public int? IdError { get; set; }

    public virtual Empleado? AsignadoANavigation { get; set; }

    public virtual Empleado? CerradoPorNavigation { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }

    public virtual Error? IdErrorNavigation { get; set; }
    public string NombreArea { get; set; }
    public string DescripcionE { get; set; }
    public string Asignado { get; set; }
    public string Cerrado { get; set; }
}

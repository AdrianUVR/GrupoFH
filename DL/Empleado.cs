using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? NombreEmpleado { get; set; }

    public string? Usuario { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Ticket> TicketAsignadoANavigations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketCerradoPorNavigations { get; set; } = new List<Ticket>();
}

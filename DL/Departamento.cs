using System;
using System.Collections.Generic;

namespace DL;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? NombreDepartamento { get; set; }

    public string? DescripcionD { get; set; }

    public int? IdArea { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }
    public string NombreArea { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {

        public int IdEmpleado { get; set; }

        public string NombreEmpleado { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public List<object> Empleados { get; set; }
    }
}

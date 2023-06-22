using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        public string NombreDepartamento  { get; set; }

        public string  DescripcionD { get; set; }

        public ML.Area Area { get; set; }

        public List<object> Departamentos { get; set; }
    }
}

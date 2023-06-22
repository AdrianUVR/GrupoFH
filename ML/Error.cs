using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Error
    {

        public int IdError { get; set; }

        public string DescripcionE { get; set; }

        public string Paso1 { get; set; }

        public string Paso2 { get; set; }


        public string Paso3 { get; set; }


        public ML.Area Area { get; set; }


        public List<object> Errores { get; set; }
    }
}

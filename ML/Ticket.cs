using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ticket
    {

        public int IdTicket { get; set; }

        public string Comentarios { get; set; }

        public string FechaAsignacion { get; set; }

        public int AsignadoA { get; set; }

        public int CerradoPor { get; set; }

        public bool Status { get; set; }

        public ML.Empleado Empleado { get; set; }


        public ML.Error Error { get; set; }

        public ML.Area Area { get; set; }

        public List<object> Tickets { get; set; }
    }
}

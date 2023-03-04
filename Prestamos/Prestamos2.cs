using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prestamos
{
    internal class Prestamos2
    {
        public int idPres { get; set; }
        public int idClient { get; set; }
        public double cantidad { get; set; }
        public double restante { get;set; }
        public DateTime fechain { get; set; }
        public DateTime fechafin { get; set; }

        public String nombre { get; set; }
        public String ape { get; set; }
    }
}

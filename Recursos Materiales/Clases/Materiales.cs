using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursos_Materiales.Clases
{
    public class Materiales
    {
        
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Id_tipo { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
    }
}

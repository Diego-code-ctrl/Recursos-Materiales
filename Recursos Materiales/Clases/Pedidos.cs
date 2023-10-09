using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursos_Materiales.Clases
{
    public class Pedidos
    {
        public int ID { get; set; }
        public DateTime Fecha_pedido { get; set; }
        public DateTime Fecha_entrega { get; set; }
        public int ID_material { get; set; }
        public int ID_departamento { get; set; }
        public int Status { get; set; }
        public int Cantidad { get; set; }
    }
}

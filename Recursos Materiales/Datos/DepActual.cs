using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursos_Materiales.Datos
{
    public static class DepActual
    {
        //Toma el departamento actual iniciado desde el login mediante el id
        public static decimal DepartamentoActual { get; set; }

        //Toma el departamento actual iniciado desde el login mediante el nombre
        public static string DepartamentoActualNombre { get; set; }
    }
}

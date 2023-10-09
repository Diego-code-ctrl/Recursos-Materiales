using Recursos_Materiales.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursos_Materiales.Datos
{
    public static class Insertar
    {
        //Método estático para la inserción de materiales
        public static void InsertarMateriales(Materiales material)
        {
            //Uso del contexto de la base de datos
            using(RecursosEntities entities = new RecursosEntities())
            {
                //Agrega el material a la entidad Materiales y guarda los cambios
                entities.Materiales.Add(material);
                entities.SaveChanges();
            }
        }

        //Método estático para la inserción de departamentos
        public static void InsertarDepartamento(Departamentos departamento)
        {
            //Uso del contexto de la base de datos
            using(RecursosEntities entities = new RecursosEntities())
            {
                //Agrega el departamento a la entidad Departamentos y guarda los cambios
                entities.Departamentos.Add(departamento);
                entities.SaveChanges();
            }
        }

        //Método estático para la inserción de pedidos
        public static void InsertarPedido(Pedidos pedido)
        {
            //Uso del contexto en la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Agrega el pedido a la entidad de Pedidos y guarda los cambios
                db.Pedidos.Add(pedido);
                db.SaveChanges();
            }
        }
    }
}

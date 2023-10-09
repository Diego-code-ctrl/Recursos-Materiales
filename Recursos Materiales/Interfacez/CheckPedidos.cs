using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Recursos_Materiales.Clases;
using Recursos_Materiales.Datos;

namespace Recursos_Materiales.Interfacez
{
    public partial class ChekPedido : Form
    {
        public ChekPedido()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y abre la ventana principal del usuario
        private void button1_Click(object sender, EventArgs e) //Botón de regreso
        {
            this.Close();
            Llamadas.LlamarInicio();
        }

        //Cuando se abre la ventana carga todos los pedidos en la tabla
        private void ChekPedido_Load(object sender, EventArgs e)
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Obtiene los datos de la base de datos de la vista <<VW_Pedidos_departamentos_material>>
                //Mediante la librería linq
                var lst = from p in db.VW_Pedidos_departamentos_material
                          where p.nombre_departamento.ToLower() == DepActual.DepartamentoActualNombre.ToLower()
                          select p;         

                //Carga los elementos a la tabla
                tablaPedidos.DataSource = lst.ToList();

            }
        }

        //Eliminar pedido para el usuario
        private void button2_Click(object sender, EventArgs e) //Eliminar button2
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                try
                {
                    //Caja que le pregunta al usuario si quiere eliminar el pedido
                    var result = MessageBox.Show("¿Seguro que desea eliminar ese pedido?",
                        "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //Resultado si sí quiere eliminar el pedido
                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(textBox1.Text);

                        //Busca el id del pedido y elimina el objeto buscado
                        Pedidos pedido = db.Pedidos.Find(id);
                        db.Pedidos.Remove(pedido);
                        db.SaveChanges();

                        //Mensaje de que se eliminó correctamente
                        MessageBox.Show("El pedido se eliminó con éxito",
                        "Pedido eliminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        //Se refresca la página
                        this.Dispose();
                        Llamadas.LlamarInicio();
                    }                   
                }
                catch (Exception ex)
                {
                    //Borta una alerta de error expresando que algo salió mal en la base de datos
                    MessageBox.Show("Algo salió mal al eliminar el pedido\n" + ex.Message,
                        "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Busca el elemento mediante su ID y lo coloca en la tabla
        private void button3_Click(object sender, EventArgs e) //Botón de buscar button3
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Validar campo completo
                if (textBox1.Text == "")
                {
                    //Brota una alerta expresando que no se admiten campos vacíos
                    MessageBox.Show("No se admiten campos vacíos",
                        "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox1.Text.All(Char.IsNumber))
                    {
                        //Convierte lo que tiene el textbox de ID a entero
                        int id = Convert.ToInt32(textBox1.Text);

                        //Buscar dato en la base de datos en la vista <<VW_Pedidos_departamentos_material>>
                        //Mediante la libe´rería linq
                        var lst = from b in db.VW_Pedidos_departamentos_material
                                  where b.id == id
                                  select b;

                        //Valida si se econtró el dato
                        if (lst.Count() > 0)
                        {
                            try
                            {
                                //Carga el dato en la tabla
                                tablaPedidos.DataSource = lst.ToList();

                                //Validar que se encuentre el dato y se activa el botón de eliminar
                                button2.Enabled = true;

                            }
                            catch (Exception ex)
                            {
                                //Error de que no se econtró el id del pedido
                                MessageBox.Show("No se econtró el id del pedido :(\n" + ex.Message,
                            "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        //Error de tipo ingresado
                        MessageBox.Show("Se esperaba un número",
                            "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Brota un mensaje expresando como se utiliza la ventana actual
        private void button4_Click(object sender, EventArgs e) //Botón de ayuda
        {
            MessageBox.Show("Se busca el pedido a partir del ID\n\n" +
                "Al darle click a la lupa encontrará el pedido deseado.\n" +
                "Al darle click al botón con el documento con tache, se eliminará automáticamente.\n" +
                "El ID debe estar registrado para buscarlo y eliminarlo."
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

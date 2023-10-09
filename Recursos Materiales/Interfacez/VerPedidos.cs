using Recursos_Materiales.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recursos_Materiales.Interfacez
{
    public partial class VerPedidos : Form
    {
        public VerPedidos()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y llama a la ventana principal del administrador
        private void button1_Click(object sender, EventArgs e) //Botón regresar
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        //Cuando se abre la ventana se cargan los datos a la tabla
        private void VerPedidos_Load(object sender, EventArgs e)
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Cargar los datos en la tabla
                tablaSoli.DataSource = db.VW_Pedidos_departamentos_material.ToList();
            }
        }

        //Brota un mensaje expresando todo lo que el usuario puede hacer
        private void button5_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Brota una alerta expresando todo lo que puede hacer el usuario en la ventana actual
            MessageBox.Show("En este apartado se muestran todos los pedidos de todos los departamentos,\n" +
                "aquí se cambian el estatus de si se entregó o no.\n\n" +
                "La lupa encontrará el pedido por ID y desbloqueará los botones de cambiar estatus.\n" +
                "El botón con el documento con la palomita verde cambiará el estatus a <<entregado>>.\n" +
                "El botón con el documento con el tache rojo cambiará el estatus a <<no entregado>>.\n" +
                "El pedido debe estar registrado para poder cambiar el estatus."
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        //Busca el pedido mediante el ID
        private void button4_Click(object sender, EventArgs e) //Buscar pedido
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Valida que el campo no esté vacío
                if (textBox1.Text == "")
                {
                    //Brota un mensaje que expresa que el textbox id está vacío
                    MessageBox.Show("El campo está vacío",
                        "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Valida que ingrese un número
                    if (textBox1.Text.All(Char.IsNumber))
                    {
                        try
                        {
                            //Convierte lo que tiene el texbox del id a número
                            int id = Convert.ToInt32(textBox1.Text);

                            //Obtiene la lista de pedidos mediante la base de datos por la vista <<VW_Pedidos_departamentos_material>>
                            //Mediante la librería linq
                            var lst = from p in db.VW_Pedidos_departamentos_material
                                      where p.id == id
                                      select p;

                            //Valida que el pedido esté en la base de datos
                            if (lst.Count() > 0)
                            {
                                //Carga los datos de la vista a la tabla
                                tablaSoli.DataSource = lst.ToList();

                                //Habilita los botones de cambiar estatus
                                button2.Enabled = true;
                                button3.Enabled = true;

                            }
                            else
                            {
                                //Brota una alerta expresando que el pedido no fue encontrado
                                MessageBox.Show("No se econtró el pedido",
                                "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        catch (Exception ex)
                        {
                            //Brota una alerta sobre un error en la base de datos
                            MessageBox.Show("Algo salió mal :( \n" + ex.Message,
                            "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //Brota un error expresando que se esperaba un número
                        MessageBox.Show("Se esperaba un número", 
                            "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Cambia el estatus a 1 (Entregado)
        private void button3_Click(object sender, EventArgs e) //Botón entregado
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Convierte lo que tiene el textbox id a número
                int id = Convert.ToInt32(textBox1.Text);

                //Se modifica el estatus a 1 y la fecha del pedido entregado
                Pedidos p = db.Pedidos.Find(id);
                p.status = 1;
                p.fecha_entregado = DateTime.Now;

                //Mediante el pedido se accede al id_material (llave foránea) y se busca mediante ese id
                Materiales m = db.Materiales.Find(p.id_material);
                m.cantidad -= p.cantidad; //Se modifica la cantidad en el Stock con la cantidad pedida

                //Se le dice a la base de datos que se modificaron los objetos del pedido y material
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); //Guarda los cambios

                //Brota una alerta expresando que se cambió el estatus
                MessageBox.Show("Se modificó el status", 
                    "Listo!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //Cierra la ventana actual y llama a la ventana principal del administrador
                this.Close();
                Llamadas.LlamarAdmin();
            }
        }

        //Cambia el estatus a 0 (No entregado)
        private void button2_Click(object sender, EventArgs e) //Botón no entregado
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Convierte lo que tiene el textbox id a número
                int id = Convert.ToInt32(textBox1.Text);

                //Se modifica el estatus a 0 y la fecha del pedido no entregado
                Pedidos p = db.Pedidos.Find(id);
                p.status = 0;
                p.fecha_entregado = DateTime.Now;

                //Se le dice a la base de datos que se cambió el objeto
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); //Guarda los cambios

                //Brota una alerta expresando que se cambió el estatus
                MessageBox.Show("Se modificó el status",
                    "Listo!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //Cierra la ventana actual y llama a la ventana principal del administrador
                this.Close();
                Llamadas.LlamarAdmin();
            }
        }
    }
}

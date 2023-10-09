using Recursos_Materiales.Clases;
using Recursos_Materiales.Datos;
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
    public partial class Pedido : Form
    {
        //Contador es una variable local para saber la cantidad que pedirá el departamento
        int _cont = 0;

        public Pedido()
        {           
            InitializeComponent();
        }

        //Cierra la ventana actual y llama a la ventana principal del usuario
        private void button4_Click(object sender, EventArgs e) //Botón de regreso
        {
            this.Close();
            Llamadas.LlamarInicio();
        }

        //Agrega n cantidad del material seleccionado según los clicks que se dé
        private void button2_Click(object sender, EventArgs e) //Botón agregar
        {
            //Valide que sea menor o igual a 10
            if (_cont >= 10)
            {
                //Brota una alerta expresando que no se puede pedir más de 10 veces ese material
                MessageBox.Show("No puedes agregar más de 10 productos",
                    "Demasiados productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label4.Text = "10";
            }
            else
            {
                //Suma uno al contador y lo pasa a string para que se vea el cambio
                _cont++;
                label4.Text = _cont.ToString();
            }
        }

        //Quita n cantidad del material seleccionado según los clicks que se dé
        private void button3_Click(object sender, EventArgs e) //Botón quitar
        {
            //Valide que sea mayor o igual a 0
            if (_cont <= 0)
            {
                //Se coloca el 0 y no pasa de ahí
                label4.Text = "0";
            }
            else
            {
                //Disminuy el contador y lo pasa a string para que se vea el cambio
                _cont--;
                label4.Text = _cont.ToString();
            }
        }

        //Carga los elementos al combobox Tipo cuadno se abre la ventana
        private void Pedido_Load(object sender, EventArgs e)
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Obtiene una lista de todos los tipos de la entidad Tipos
                //Mediante la librería linq
                var lst = from p in db.Tipos
                          select p;

                //Agrega todos los nombres de los elementos de la lista al commbobox Tipo
                foreach (var item in lst)
                {
                    comboBox1.Items.Add(item.caracteristica);
                }
            }
        }

        //Sucede cuando el combobox Tipo cambia de objeto
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se limpian los componentes
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Se selecciona el tipo de material
                int index = comboBox1.SelectedIndex + 1;

                //Obtiene una lista de materiales de la entidad Materiales
                //Mediante la librería linq
                var lst2 = from m in db.Materiales
                           where m.id_tipo == index
                           select m;

                foreach (var item in lst2)
                {
                    //Agrega el nombre del material al combobox Material
                    comboBox2.Items.Add(item.nombre);
                }
            }
        }

        //Al hacer click en este botón se insertará el pedido del departamento loggeado
        private void button1_Click(object sender, EventArgs e) //Botón hacer pedido
        {
            //Uso del contexto de la base de datos
            using(RecursosEntities db = new RecursosEntities())
            {
                //Validación que todos los campos estén llenos
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    //Brota un mensaje expresando que dejó campos vacíos
                    MessageBox.Show("Error al pedir materiales",
                        "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Validación de cantidad correcta
                    if (label4.Text == "0")
                    {
                        //Brota un mensaje por la cantidad elegida
                        MessageBox.Show("La canitdad mínima debe ser 1",
                            "Cantidad no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {
                            int idMaterial = 0;
                            //Se llenan las propiedades del objeto con los componentes y las fechas
                            var pedido = new Pedidos();
                            pedido.fecha_pedido = DateTime.Now;
                            pedido.fecha_entregado = DateTime.Now.AddDays(2);
                            Materiales m = db.Materiales.Where(ma => ma.id_tipo == comboBox1.SelectedIndex + 1).First();
                            idMaterial = (int)m.id_materiales;
                            pedido.id_material = idMaterial;
                            pedido.id_departamento = DepActual.DepartamentoActual;
                            pedido.status = 0;
                            pedido.cantidad = Convert.ToInt32(label4.Text);

                            //Se inserta el pedido
                            Insertar.InsertarPedido(pedido);

                            //Brota una alerta expresando que el pedido se insertó
                            MessageBox.Show("Solicitud enviada con éxito",
                                "Pedido enviado!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //Se limpian los componentes
                            comboBox1.SelectedIndex = -1;
                            comboBox2.SelectedIndex = -1;
                            comboBox2.Items.Clear();
                            comboBox1.Items.Clear();
                            label4.Text = "0";
                        }
                        catch (Exception ex)
                        {
                            //Brota un error porque algo ocurrió en la base de datos
                            MessageBox.Show("Error al insertar pedido " + ex.Message,
                                "Algo salió mal...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //Mensaje para que el usuario sepa qué puede hacer en esta ventana
        private void button5_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Brota un mensaje expresando lo que el usuario puede hacer en la ventana actual
            MessageBox.Show("Es necesario llenar los campos con las siguientes características\n\n" +
                "La cantidad debe ser mayor a 0 y menor que 11.\n" +
                "Ningún campo debe estar vacío."
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

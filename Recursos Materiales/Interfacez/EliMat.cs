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

namespace Recursos_Materiales.Interfacez
{
    public partial class EliMat : Form
    {
        public EliMat()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y abre la ventana principal del administrador
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        //Elimina el material seleccionado con el combobox del material
        private void button1_Click(object sender, EventArgs e) //Botón eliminar
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Valide que seleccione un material
                if (comboBox1.Text == "")
                {
                    //Brota una alerta expresando que no ha seleccionado ningún material
                    MessageBox.Show("No se seleccionó ningún material", 
                        "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        //Se obtiene el nombre del material del combobox
                        string index = comboBox1.Text;
                        int id = 0;

                        //Se obtiene la lista desde la entidad de Materiales
                        var lst = from d in db.Materiales //Selecciona el material del query en linq
                                  where d.nombre == index
                                  select d;

                        foreach (var item in lst) //Saca el elemento individualmente
                        {
                            if (item.nombre == index)
                            {
                                id = (int)item.id_materiales;
                                Materiales dep = db.Materiales.Find(id); //Encuentra el objeto mediante el id 
                                db.Materiales.Remove(dep); //Elimina el material
                                break;
                            }
                        }
                        db.SaveChanges(); //Se guardan los cambios

                        //Brota una alerta expresando que el material se eliminó correctamente
                        MessageBox.Show("El material se ha eliminado con exito",
                            "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        comboBox1.Items.Remove(index); //Se retira el item del combobox
                    }
                    catch (Exception ex)
                    {
                        //Brota un mensaje de error porque algo salió mal en la base de datos
                        MessageBox.Show("No se pudo eliminar el material " + ex.Message,
                            "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Carga los nombres de los materiales cuando se abre la ventana en el combobox
        private void EliMat_Load(object sender, EventArgs e)
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Obtiene todos los elementos de la entidad Materiales
                //Mediante la librería linq
                var lst = from d in db.Materiales
                          select d;

                //Agrega las materiales al combobox
                foreach (var item in lst)
                {
                    comboBox1.Items.Add(item.nombre);
                }
            }
        }

        //Brota un mensaje expresando todo lo que puede hacer el usuario en la ventana actual
        private void button4_Click(object sender, EventArgs e) //BOtón de ayuda
        {
            //Brota un mensaje para auxiliar al usuario sobre el uso de la ventana
            MessageBox.Show("En este apartado se elimina el material registrado\n\n" +
                "Antes de eliminar el material verifique que no tenga algún pedido."
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

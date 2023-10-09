using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Recursos_Materiales.Clases;
using Recursos_Materiales.Datos;

namespace Recursos_Materiales.Interfacez
{
    public partial class EliDep : Form
    {
        public EliDep()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y llama a la ventana principal del administrador
        private void button2_Click(object sender, EventArgs e) //Botón de regresar
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Eliminación del departamento
            using (RecursosEntities db = new RecursosEntities())
            {
                //Validación de selección de un departamento
                if (comboBox1.Text == "")
                {
                    //Brota una alerta expresando que no seleccionó ningún departamento
                    MessageBox.Show("No se seleccionó ningún departamento",
                        "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        //Se obtiene el nombre del combobox
                        string index = comboBox1.Text;
                        int id = 0;

                        //Obtiene la lista de la entidad Departamentos
                        var lst = from d in db.Departamentos //Consulta para el query obtenido de linq
                                  where d.nombre_departamento == index
                                  select d;

                        foreach (var item in lst) //Se obtiene el elemento individual
                        {
                            if (item.nombre_departamento == index)
                            {
                                id = (int)item.id;
                                Departamentos dep = db.Departamentos.Find(id); //Encuentra el objeto mediante el id 
                                db.Departamentos.Remove(dep); //Se elimina el departamento buscado
                                break;
                            }
                        }
                        db.SaveChanges(); //Se guardan los cambios

                        //Brota una alerta expresando que se eliminó correctamente el departamento
                        MessageBox.Show("El departamento se ha eliminado con exito",
                            "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        comboBox1.Items.Remove(index); //Se retira el item del combobox
                    }
                    catch (Exception)
                    {
                        //Brota un mensaje de error porque algo de la base de datos salió mal
                        MessageBox.Show("No se pudo eliminar el departamento \n"
                            + "Asegúrese de que el departamento a eliminar ya no\n"
                            + "tenga pedidos pendientes",
                            "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Cuando se abre la ventana se cargan los datos al combobox
        private void EliDep_Load(object sender, EventArgs e)
        {
            //Cargar datos de la base de datos en el ComboBox
            using (RecursosEntities db = new RecursosEntities())
            {
                //Obtiene todos los datos de la entidad Departamentos
                //Mediante la librería linq
                var lst = from d in db.Departamentos
                          select d;

                //Agrega todos los nombres de los elementos de la lista al combobox 
                foreach (var item in lst)
                {
                    comboBox1.Items.Add(item.nombre_departamento);
                }
            }
        }

        //Brota un mensaje de ayuda expresando el uso de la ventana actual
        private void button4_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Brota una alerta expresando el cómo se debe usar la ventana actual
            MessageBox.Show("En este apartado se selecciona el departamento a eliminar\n\n" +
                "Si no se elimina verifique que el departamento no tenga ningún pepdido.\n"
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

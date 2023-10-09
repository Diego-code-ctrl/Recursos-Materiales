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
//using Recursos_Materiales.Clases;

namespace Recursos_Materiales.Interfacez
{
    public partial class ConDep : Form
    {
        public ConDep()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y abre la ventana principal del administrador
        private void button3_Click(object sender, EventArgs e) //Botón de regreso
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        //Cuando se abre la ventana carga los elementos a la tabla
        private void ConDep_Load(object sender, EventArgs e)
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Carga los datos de la base de datos de la vista <<VW_Edificios_departamentos>>
                tablaDep.DataSource = db.VW_Edificios_departamentos.ToList();
            }
        }

        //Brota una alerta expresando el uso de la ventana actual
        private void button4_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Mensaje expresando el cómo se usa la ventana actual
            MessageBox.Show("En este apartado se enceuntrarn todos los departamentos registrados"
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

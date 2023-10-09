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
    public partial class ConMat : Form
    {
        public ConMat()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y manda a llamar a la ventana principal del administrador
        private void button3_Click(object sender, EventArgs e) //Botón de regresar
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        //Cuando se abre la ventana carga los datos a la tabla
        private void ConMat_Load(object sender, EventArgs e)
        {
            //Uso del contexto actual de la base de datos
            using(RecursosEntities db = new RecursosEntities())
            {
                //Carga en la tabla los datos que contenga la vista <<VW_Materiales_Tipos>>
                tablaMat.DataSource = db.VW_Materiales_Tipos.ToList();
            }
        }

        //Brota una alerta ayudando al usuario a cómo utilizar la ventana actual
        private void button4_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Brota un mensaje expresando el uso de la ventana actual
            MessageBox.Show("En este apartado se enceuntran todos los materiales registrados"
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

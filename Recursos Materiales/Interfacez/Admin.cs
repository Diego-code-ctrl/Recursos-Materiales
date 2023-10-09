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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        //Llama a la ventana de consultar materiales y cierra la ventana actual
        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {   
            this.Close();
            Llamadas.LlamarConMat();
        }

        //Llama a la ventana de login y cierra la ventana actual
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarLogin();
        }

        //Llama a la ventana agregar departamentos y cierra la ventana actual
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarAgDep();
        }

        //Llama a la ventana eliminar departamento y cierra la ventana actual
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarEldep();
        }

        //Llama a la ventana agregar materiales y cierra la ventana actual
        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarAgMat();
        }

        //Llama a la ventana eliminar materiales y cierra la ventana actual
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarEliMat();
        }

        //Llama a la ventana consultar departamentos y cierra la ventana actual
        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarConDep();
        }

        //Llama a la ventana de ver pedidos y cierra la ventana actual
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarVerPedidos();
        }

        //El botón de ayuda muestra el cómo se usará la ventana 
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El menú departamentos despliega opciones para agregar, eliminar o consultar los departamentos.\n"
                + "El menú materiales despliega opciones para agregar, eliminar o consultar los nateriales.\n"
                + "El menú pedidos despliega la opción de consultar y de actualizar pedidos.",
                "Ayuda",MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

using Recursos_Materiales.Clases;
using Recursos_Materiales.Interfacez;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recursos_Materiales
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y llama a la ventana de login
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Llamadas.LlamarLogin();
        }

        //Cierra la ventana actual y llama a la ventana CheckPedidos
        private void button1_Click(object sender, EventArgs e) //Botón Revisar pedidos
        {
            this.Close();
            Llamadas.LlamarCheckPedidos();
        }

        //Cierra la ventana actual y llama a la ventana de Pedido
        private void realizarPedido_Click(object sender, EventArgs e) //Botón Realizar pedidos
        {
            this.Close();
            Llamadas.LlamarPedido();
        }
    }
}

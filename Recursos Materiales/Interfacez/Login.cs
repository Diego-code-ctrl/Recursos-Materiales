using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Recursos_Materiales.Clases;
using Recursos_Materiales.Datos;

namespace Recursos_Materiales
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //Botón para iniciar sesión mediante la base de datos
        private void buttonAceptar_Click(object sender, EventArgs e) //Botón aceptar
        {
            //Uso del contexto de la base de datos
            using(RecursosEntities db = new RecursosEntities())
            {
                //Se obtienen los datos que tienen los componentes (textbox's)
                string usuario = textBoxUser.Text;
                string contra = textBoxPassword.Text;
                decimal id = 0;

                if (usuario == "admin" && contra == "admin") //Usuario administrador (predeterminado)
                {
                    //Esconde la ventana actual (Para que no se cierre el programa) y manda a llamar
                    //a la ventana principal del administrador
                    Llamadas.LlamarAdmin();
                    this.Hide();
                }
                else
                {
                    //Consulta de usuario existente y su contraseña mediante la librería linq
                    var lst = from d in db.Departamentos
                              where d.nombre_departamento == usuario
                              && d.contrasena == contra
                              select d;

                    foreach(var item in lst) //Extrae el elemento seleccionado
                    {
                        id = item.id;
                        break;
                    }

                    //Valida si el usuario existe en la base de datos
                    if (lst.Count() > 0)
                    {
                        DepActual.DepartamentoActual = id; //Se obtiene el id de ese departamento en la variable estatica
                        DepActual.DepartamentoActualNombre = usuario; //Se obtiene el nombre de ese departamento en la variable estática
                        
                        //Esconde la ventana actual y llama a la ventana principal del usuario/departamento
                        Llamadas.LlamarInicio();
                        this.Hide();
                    }
                    else
                    {
                        //Brota un error que menciona que alguno de los dos campos están incorrectos
                        MessageBox.Show("Usuario o contraseña incorrectos",
                            "Error al ingresar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Cierra la aplicación totalmente
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

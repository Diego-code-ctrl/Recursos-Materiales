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
    public partial class AgDep : Form
    {
        public AgDep()
        {
            InitializeComponent();
        }

        //Llama a la ventana principal de adminsitrador y cierra la ventana actual
        private void button2_Click(object sender, EventArgs e) //Botón de regresar
        {
            Llamadas.LlamarAdmin();
            this.Close();           
        }

        private void button1_Click(object sender, EventArgs e) //Botón de agregar departamento
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Se le asignan todos los atributos a la clase para posteriormente insertar el objeto
                var dep = new Departamentos();
                dep.nombre_departamento = textBox1.Text;
                dep.contrasena = textBox2.Text;
                dep.nombre_encargado = textBox3.Text;
                dep.id_edificio = comboBox1.SelectedIndex + 1;

                //Validación de campos vacíos
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
                {
                    //Brota una alerta expresando que no se admiten campos vacíos
                    MessageBox.Show("No se admiten campos vacíos",
                        "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Validación de carácteres especiales y tipos de datos
                    if (!(textBox1.Text.All(Char.IsLetter)) || !(textBox3.Text.All(Char.IsLetter)))
                    {
                        //Brota una alerta expresando que no se admiten caracteres especiales en ciertos campos
                        MessageBox.Show("Los campos no deben contener caracteres especiales\n o deben ser letras\n" +
                            "exceptuando la contraseña", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.SelectedIndex = -1;
                    }
                    else
                    {
                        try
                        {
                            //Saca a todos los elementos de la base de datos de la entidad <<Departamentos>>
                            //Mediante la librería linq
                            var lst = from d in db.Departamentos
                                      where textBox1.Text == d.nombre_departamento
                                      select d;

                            //Valida que ya exista el departamento
                            if (lst.Count() != 1)
                            {
                                //Se inserta el departamento mediante una clase estática
                                Insertar.InsertarDepartamento(dep);

                                //Brota un mensaje de que el departamento se agregó con exito
                                MessageBox.Show("El departamento se agregó con exito", "Éxito al insertar",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                //Se vacían los componentes
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                comboBox1.SelectedIndex = -1;
                                
                            }
                            else
                            {
                                //Validación de que no se ingrese un departamento ya existente
                                MessageBox.Show("El departamento ya existe",
                                "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //se vacían los componentes
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                comboBox1.SelectedIndex = -1;
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            //Error que brota si algo salió mal en la base de datos
                            MessageBox.Show("Algo salió mal :( \n error: " + ex.Message,
                                "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //Verificar si contiene un objeto el texbox de nombre del departamento
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Si el textbox contiene un objeto la palomita se pondrá verde
            if (!(textBox1.Text == ""))
            {
                pictureBox5.Visible = true;
                pictureBox1.Visible = false;
            }
            else
            {
                //Si el textbox esstá vacío la palomita se pondrá en gris
                pictureBox5.Visible = false;
                pictureBox1.Visible=true;
            }
        }

        //Verificar si tiene contiene un objeto el textbox de contrasena
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Si el textbox tiene un objeto la palomita se pondrá en verde
            if (!(textBox2.Text == ""))
            {
                pictureBox6.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                //Si el textbox está vacío se pondrá en gris la palomita
                pictureBox6.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        //Verificar si tiene contiene un objeto el textbox de nombre del encargado
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Si el textbox tiene un objeto se pondrá en verde la palomita
            if (!(textBox3.Text == ""))
            {
                pictureBox7.Visible = true;
                pictureBox3.Visible = false;
            }
            else
            {
                //Si el textbox no tiene un objeto se pondrá en gris la palomita
                pictureBox7.Visible = false;
                pictureBox3.Visible = true;
            }
        }

        //Verificar si tiene contiene un objeto el combobox edificio
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si el combobox tiene un objeto seleccionado la palomita se pondrá en verde
            if (!(comboBox1.SelectedIndex == -1))
            {
                pictureBox8.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                //Si el combobox no tienen un objeto seleccionado la palomita se pondrá gris
                pictureBox8.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        //Muestra al usuario cómo utilizar la ventana
        private void button3_Click(object sender, EventArgs e) //Botón de ayuda
        {
            MessageBox.Show("Es necesario llenar los campos con las siguientes características\n\n" + 
                "Nombre del departamento solo acepta letras.\n" +
                "El nombre del encargado solo acepta letras.\n" + 
                "Los campos no deben estar vacíos.\n" + 
                "No se pueden registrar más veces un departamento."
                ,"Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

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
using Recursos_Materiales.Datos;

namespace Recursos_Materiales.Interfacez
{
    public partial class AgMat : Form
    {
        public AgMat()
        {
            InitializeComponent();
        }

        //Cierra la ventana actual y abre la ventana principal del administrador
        private void button2_Click(object sender, EventArgs e) //Botón de regresar
        {
            this.Close();
            Llamadas.LlamarAdmin();
        }

        //Inserta el material en la entidad Materiales
        private void button1_Click(object sender, EventArgs e) //Botón de insertar material
        {
            //Uso del contexto de la base de datos
            using (RecursosEntities db = new RecursosEntities())
            {
                //Validación de que sean los tipos de datos insertados en los campos correspondientes
                try
                {
                    //Se crea el objeto que se va a insertar
                    var material = new Materiales();
                    material.nombre = textBox1.Text;
                    material.id_tipo = comboBox1.SelectedIndex + 1;
                    material.cantidad = Convert.ToInt32(textBox3.Text);
                    material.costo = Convert.ToDecimal(textBox2.Text);

                    //Validación de campos vacíos
                    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
                    {
                        //Mensaje de no se admiten campos vacíos
                        MessageBox.Show("No se admiten campos vacíos",
                            "Error de campos vaciós", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Validación de caracteres especiales
                        if (!(textBox1.Text.All(Char.IsLetter)) || !(textBox3.Text.All(Char.IsNumber)) || !(textBox2.Text.All(Char.IsNumber)))
                        {
                            //Brota una alerta expresando que no se admiten otro tipo de datos en ciertos campos
                            MessageBox.Show("Algún campo tiene un caracter especial no válido",
                                "Error de caracteres especiales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            try
                            {
                                //Se obtiene todos los elementos de la entidad Materiales
                                //Mediante la librería linq
                                var lst = from m in db.Materiales
                                          where m.nombre == textBox1.Text
                                          select m;

                                //Validar que el material ya existe
                                if (lst.Count() != 1)
                                {
                                    //Inserta el material mediante una clase estática
                                    Insertar.InsertarMateriales(material);

                                    //Brota una alerta expresando que el material se insertó correctamente
                                    MessageBox.Show("Material agregado correctamente", "Éxito al insertar",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    //Se vacían los campos
                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    comboBox1.SelectedIndex = -1;
                                }
                                else
                                {
                                    //Brota una alerta expresando que el material insertado ya existe en la base de datos
                                    MessageBox.Show("El material ya existe", "Error al insertar",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    //Se vacían los campos
                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    comboBox1.SelectedIndex = -1;
                                }
                            }
                            catch (Exception ex)
                            {
                                //Brota un mensaje expresando que algo salió mal en la base de datos
                                MessageBox.Show("Algo salió mal :( \nerror:" + ex.Message, "Error al insertar",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Brota un mensaje expresando que algo salió mal en la base de datos
                    MessageBox.Show("Se colocó mal el dato un campo\n" +
                        ex.Message, "Error de dato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Cambia el estado de la palomita del textbox de Nombre
        private void textBox1_TextChanged(object sender, EventArgs e) //Nombre textbox
        {
            //Si hay un objeto en el textbox la palomita se pondrá en verde
            if (!(textBox1.Text == ""))
            {
                pictureBox5.Visible = true;
                pictureBox1.Visible = false;
            }
            else
            {
                //Si no hay nada en el textbox la palomita se pondrá en gris
                pictureBox5.Visible = false;
                pictureBox1.Visible = true;
            }
        }

        //Cambia el estado de la palomita del combobox de Tipo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Tipo combobox
        {
            //Si hay un objeto en el combobox la palomita se pondrá en verde
            if (!(comboBox1.SelectedIndex == -1))
            {
                pictureBox6.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                //Si no hay nada en el combobox la palomita se pondrá en gris
                pictureBox6.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        //Cambia el estado de la palomita del textbox de Cantidad
        private void textBox3_TextChanged(object sender, EventArgs e) //Cantidad textbox
        {
            //Si hay un objeto en el textbox la palomita se pondrá en verde
            if (!(textBox3.Text == ""))
            {
                pictureBox7.Visible = true;
                pictureBox3.Visible = false;
            }
            else
            {
                //Si no hay nada en el textbox la palomita se pondrá en gris
                pictureBox7.Visible = false;
                pictureBox3.Visible = true;
            }
        }

        //Cambia el estado de la palomita del textbox de Costo
        private void textBox2_TextChanged(object sender, EventArgs e) //Costo textbox
        {
            //Si hay un objeto en el textbox la palomita se pondrá en verde
            if (!(textBox2.Text == ""))
            {
                pictureBox8.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                //Si no hay nada en el textbox la palomita se pondrá en gris
                pictureBox8.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e) //Botón de ayuda
        {
            //Brota una alerta explicando al usuario como manipular la ventana
            MessageBox.Show("Es necesario llenar los campos con las siguientes características\n\n" +
                "Nombre del material solo acepta letras.\n" +
                "La cantidad solo acepta números enteros.\n" +
                "El costo solo acepta números enteros.\n" +
                "No se pueden registrar más veces un material."
                , "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

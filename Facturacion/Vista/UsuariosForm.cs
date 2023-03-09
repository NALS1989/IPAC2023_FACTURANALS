using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class UsuariosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }

        string tipooperacion= String.Empty;
        private void NuevoButton_Click(object sender, EventArgs e)
        {
            HabilitarControles();// Aqui llamamos el metodo
            GuardarButton.Enabled = true;
            tipooperacion = "Nuevo";

          
        }

        // creamos un metodo para habilitar los controles 
        private void HabilitarControles()
        {
            CodigoTextBox1.Enabled = true;
            NombreTextBox2.Enabled = true;
            contrasenaTextBox3.Enabled = true;
            CorreoTextBox4.Enabled = true;
            RolComboBox1.Enabled = true;
            EstaactivoCheckBox1.Enabled = true;
            BuscarButton.Enabled = true;
            CancelarButton.Enabled = true;



        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesaHabilitarControles();
            LimpiarControles();
            ModificarButton.Enabled = false; 
            GuardarButton.Enabled=false;
            EliminarButton.Enabled=false;
            CancelarButton.Enabled = false;


        }

        private void DesaHabilitarControles()
        {
            CodigoTextBox1.Enabled = false;
            NombreTextBox2.Enabled = false;
            contrasenaTextBox3.Enabled = false;
            CorreoTextBox4.Enabled = false;
            RolComboBox1.Enabled = false;
            EstaactivoCheckBox1.Enabled = false;
            BuscarButton.Enabled = false;
            FotoPictureBox.Image = null;



        }

        private void LimpiarControles()
        {
            CodigoTextBox1.Clear();
            NombreTextBox2.Clear();
            contrasenaTextBox3.Clear();
            CorreoTextBox4.Clear();
            RolComboBox1.Text= "";
            EstaactivoCheckBox1.Checked=false;
           



        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipooperacion = "modificar";
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (tipooperacion== "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox1.Text))

                    errorProvider1.SetError(CodigoTextBox1, "ingrese el codigo");
                CodigoTextBox1.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(NombreTextBox2.Text))
            { 
                errorProvider1.SetError(NombreTextBox2, "ingrese un nombre");
            NombreTextBox2.Focus();
            return;

            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(contrasenaTextBox3.Text))
            {
                errorProvider1.SetError(contrasenaTextBox3, "ingrese una contraseña");
                contrasenaTextBox3.Focus();
                return;

            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(RolComboBox1.Text))
            {
                errorProvider1.SetError(RolComboBox1, "Seleccione un Rol");
                RolComboBox1.Focus();
                return;

            }
            errorProvider1.Clear();


            Usuario user = new Usuario();


            user.CodigoUsuario= CodigoTextBox1.Text;
            user.Nombre = NombreTextBox2.Text;
            user.Contraseña= contrasenaTextBox3.Text;
            user.Correo = CorreoTextBox4.Text;
            user.Rol = RolComboBox1.Text;
            user.EstaActivo = EstaactivoCheckBox1.Checked;


            if (FotoPictureBox.Image != null)

            {
                System.IO.MemoryStream ms= new System.IO.MemoryStream();
                FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            // Insertar en la base de datos 



        }
             

         
    }
    


}




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void ImagenPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void login_Activated(object sender, EventArgs e)
        {
            CodigoUsuariotextBox.Focus();
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Aceptarbutton_Click(object sender, EventArgs e)
        {

            if ( CodigoUsuariotextBox.Text == string.Empty )
            {
                errorProvider1.SetError(CodigoUsuariotextBox, "Ingrese un usuario");
                return;
            }
            errorProvider1.Clear();

            if (ContraseñatextBox.Text == string.Empty )
            {
                errorProvider1.SetError(ContraseñatextBox, "Ingrese un usuario");
                return;
            }
            errorProvider1.Clear();// UNA VEZ LLENANDO LOS DATOS LIMPIA Y CONTINUA
        }

        private void Mostrarbutton_Click(object sender, EventArgs e)
        {
            if (ContraseñatextBox.PasswordChar == '*')
            {
                ContraseñatextBox.PasswordChar = '\0';//valida los valores nulos
            }

            else
            {
                ContraseñatextBox.PasswordChar = '*';// si no la tiene que la coloque

            }
        }
    }
}

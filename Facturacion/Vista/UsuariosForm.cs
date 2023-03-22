using Datos;
using Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
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

        DataTable dt = new DataTable();
        UsuarioDB UsuarioDB = new UsuarioDB();
        Usuario user = new Usuario();



        private void NuevoButton_Click(object sender, EventArgs e)
        {
            HabilitarControles();// Aqui llamamos el metodo
            CodigoTextBox1.Focus();
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
            ModificarButton.Enabled = false;



        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
           DesaHabilitarControles();
            LimpiarControles();


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
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;



        }

        private void LimpiarControles()
        {
            CodigoTextBox1.Clear();
            NombreTextBox2.Clear();
            contrasenaTextBox3.Clear();
            CorreoTextBox4.Clear();
            RolComboBox1.Text= "";
            EstaactivoCheckBox1.Checked=false;
            FotoPictureBox.Image = null;



        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipooperacion = "modificar";

            tipooperacion = "Modificar";
            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox1.Text = UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString();
                NombreTextBox2.Text = UsuariosDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                contrasenaTextBox3.Text = UsuariosDataGridView.CurrentRow.Cells["Contrasena"].Value.ToString();
                CorreoTextBox4.Text = UsuariosDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
               RolComboBox1.Text = UsuariosDataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                EstaactivoCheckBox1.Checked = Convert.ToBoolean(UsuariosDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                byte[] miFoto = UsuarioDB.DevolverFoto(UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                if (miFoto.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(miFoto);
                    FotoPictureBox.Image = System.Drawing.Bitmap.FromStream(ms);
                }

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }




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
                user.Foto = ms.GetBuffer();


            }

            // Insertar en la base de datos 
           
         



        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog=new OpenFileDialog(); 

            DialogResult dialogResult=dialog.ShowDialog();  

            if (dialogResult == DialogResult.OK)
            {
                FotoPictureBox.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void UsuariosForm_Load(object sender, System.EventArgs e)
            {
                TraerUsuarios();

            }

        private void TraerUsuarios()
            {
                dt = UsuarioDB.DevolverUsuarios();

                UsuariosDataGridView.DataSource = dt;

            }


        private void EliminarButton_Click(object sender, EventArgs e)
        {

            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bool elimino = UsuarioDB.Eliminar(UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DesaHabilitarControles();
                        TraerUsuarios();
                        MessageBox.Show("Registro eliminado");
                    }
                    else
                    { MessageBox.Show("No se pudo eliminar el registro"); }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }



        }
    }
    


}




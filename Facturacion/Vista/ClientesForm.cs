using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClientesForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }
        string tipoOperacion;

        DataTable dt = new DataTable();
        ClienteDB ClientesDB = new ClienteDB();
        Cliente client = new Cliente();


        private void HabilitarControles()
        {
            IdentidadTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            TelefonoTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            DireccionTextBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            BuscarImagenButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }

        private void DeshabilitarControles()
        {
            IdentidadTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            TelefonoTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            DireccionTextBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            BuscarImagenButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = false;
        }


        private void LimpiarControles()
        {
            IdentidadTextBox.Clear();
            NombreTextBox.Clear();
            DireccionTextBox.Clear();
            CorreoTextBox.Clear();
             EstaActivoCheckBox.Checked = false;
            AgregarPictureBox.Image = null;
        }






        private void NuevoButton_Click(object sender, EventArgs e)
        {

            IdentidadTextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";

        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(IdentidadTextBox.Text))
                {
                    errorProvider1.SetError(IdentidadTextBox, "Ingrese Identidad de Cliente");
                    IdentidadTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un nombre");
                    NombreTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
               
             

                if (AgregarPictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    AgregarPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    client.Foto = ms.GetBuffer();
                }

                //Insertar en la base de datos

                bool inserto = ClienteDB.Insertar(client);

                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                   
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro");
                }
            }
            else if (tipoOperacion == "Modificar")
            {
               client.Identidad  = IdentidadTextBox.Text;
                client.Nombre = NombreTextBox.Text;
                client.Direccion = DireccionTextBox.Text;
                
                client.Correo = CorreoTextBox.Text;
                client.EstaActivo = EstaActivoCheckBox.Checked;

                if (AgregarPictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                   AgregarPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
               
                }

                bool modifico = UsuarioDB.Editar(client);
                if (modifico)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                   
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }

                  }
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
                {
                    tipoOperacion = "Modificar";
                    if ( dataGridView1.SelectedRows.Count > 0)
                    {
                        IdentidadTextBox.Text = dataGridView1.CurrentRow.Cells["Identidad"].Value.ToString();
                        NombreTextBox.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                        
                        CorreoTextBox.Text = dataGridView1.CurrentRow.Cells["Correo"].Value.ToString();
                        
                        EstaActivoCheckBox.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["EstaActivo"].Value);

                        byte[] miFoto = ClienteDB.DevolverFoto(dataGridView1.CurrentRow.Cells["Identidad"].Value.ToString());

                        if (miFoto.Length > 0)
                        {
                            MemoryStream ms = new MemoryStream(miFoto);
                            AgregarPictureBox.Image = System.Drawing.Bitmap.FromStream(ms);
                        }

                        HabilitarControles();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un registro");
                    }
                }

                private void AdjuntarFotoButton_Click(object sender, System.EventArgs e)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    DialogResult resultado = dialog.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        AgregarPictureBox.Image = Image.FromFile(dialog.FileName);
                    }
                }

           
                private void EliminarButton_Click(object sender, EventArgs e)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                        if (resultado == DialogResult.Yes)
                        {
                            bool elimino = ClienteDB.Eliminar(dataGridView1.CurrentRow.Cells["Identidad"].Value.ToString());

                            if (elimino)
                            {
                                LimpiarControles();
                                DeshabilitarControles();
                              
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
   

    


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }

        string operacion;
        private void NuevoButton_Click(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();

        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;  
            ExistenciaTextBox.Enabled = true;   
            PrecioTextBox.Enabled = true;   
            BuscarButton.Enabled = true;    
            GuardarButton.Enabled = true;   
            CancelarButton.Enabled=true;
            NuevoButton.Enabled = false; 


        }

        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            BuscarButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled=true;

        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            operacion = "Modificar";
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {

            if(operacion=="Nuevo")
            {
                if(string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un codigo");
                    CodigoTextBox.Focus();
                    return;
                }

                errorProvider1.Clear();
                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese un descripcion");
                    return;
                   DescripcionTextBox.Focus();
                }

                errorProvider1.Clear();






            }





        }

        private void ExistenciaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.Clear();
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(ExistenciaTextBox, "Ingrese valores Numericos");
                    
                   
            }


        }

        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar !='.'))
            {
                e.Handled = true;
                errorProvider1.SetError(PrecioTextBox, "Ingrese valores Numericos");
               
            }
            else
            {
                e.Handled = false;
                errorProvider1.Clear();


            }

            if ((e.KeyChar=='.') && ((sender as TextBox).Text.IndexOf('.')>-1))

            {
                e.Handled=true; 
            }




        }
    }
}

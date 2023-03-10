using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {


        public string codigo { get; set; }  

        public string Descripcion { get; set; }  
        
        public int  Existencia { get; set; }

        public   decimal Precio { get; set; } 
        
        public byte[] Imagen { get; set; }

        public Producto()
        {
        }

        public Producto(string codigo, string descripcion, int existencia, decimal precio, byte[] imagen)
        {
            this.codigo = codigo;
            Descripcion = descripcion;
            Existencia = existencia;
            Precio = precio;
            Imagen = imagen;
        }
    }
}

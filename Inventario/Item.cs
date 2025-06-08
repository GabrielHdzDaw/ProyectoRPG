using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    public abstract class Item
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }

        public Item(string nombre, string descripcion, int precio)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Precio = precio;
        }

       

        public override string ToString()
        {
            return "Item: " + Nombre + " Descripcion: " + Descripcion + " Precio: " + Precio;
        }


    }
}

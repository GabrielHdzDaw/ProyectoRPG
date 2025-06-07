using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    public abstract class Item
    {
        string nombre;
        string descripcion;
        int precio;

        public Item(string nombre, string descripcion, int precio)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public string GetDescripcion()
        {
            return descripcion;
        }

        public int GetPrecio()
        {
            return precio;
        }

        public void SetNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public void SetDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public void SetPrecio(int precio)
        {
            this.precio = precio;
        }

        public override string ToString()
        {
            return "Item: " + nombre + " Descripcion: " + descripcion + " Precio: " + precio;
        }


    }
}

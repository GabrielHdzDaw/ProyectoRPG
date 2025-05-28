using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    internal class InventarioPersonaje
    {
        public List<Item> objetos { get; set; }
        public InventarioPersonaje()
        {
            objetos = new List<Item>();
        }
        public void AgregarObjeto(Item objeto)
        {
            objetos.Add(objeto);
        }
        public void EliminarObjeto(Item objeto)
        {
            objetos.Remove(objeto);
        }

        public bool ContieneObjeto(Item objeto)
        {
            return objetos.Contains(objeto);
        }

        public List<Item> ObtenerObjetos()
        {
            return objetos;
        }
        public override string ToString()
        {
            return string.Join(", ", objetos);
        }
        
    }
}

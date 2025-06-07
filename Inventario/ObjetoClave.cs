using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    public class ObjetoClave: Item
    {
        public ObjetoClave(string nombre, string descripcion, int precio) 
            : base(nombre, descripcion, precio)
        {
        }
        public override string ToString()
        {
            return "Objeto Clave: " + base.ToString();
        }
    }
}

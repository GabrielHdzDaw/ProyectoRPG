using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    internal class Armadura: Item
    {
        int defensa { get; set; } 
        public Armadura(string nombre, string descripcion, int precio, int defensa, int peso)
            : base(nombre, descripcion, precio)
        {
            this.defensa = defensa;
        }
        
        public override string ToString()
        {
            return $"{base.ToString()} (Defensa: {defensa})";
        }
    }
}

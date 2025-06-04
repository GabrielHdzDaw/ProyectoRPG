using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    internal class Pocion: Item
    {
        public int curacion { get; set; }
        public Pocion(int curacion)
            : base("Poción pequeña", "", 10)
        {
            this.SetDescripcion("Poción que cura " + curacion + " puntos de vida al que la bebe.");
            this.curacion = curacion;
        }
        public override string ToString()
        {
            return  $" {GetNombre()} Curación: {curacion}";
        }
    }
}

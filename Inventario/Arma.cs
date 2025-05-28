using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{

    internal class Arma: Item
    {
        public int dano { get; set; }
        public int danoCritico { get; set; }
        public Arma(string nombre, string descripcion, int precio, int dano, int danoCritico)
            : base(nombre, descripcion, precio)
        {
            this.dano = dano;
            this.danoCritico = danoCritico;
        }
        public override string ToString()
        {
            return base.ToString() + $" Daño: {dano}, Daño crítico: {danoCritico}";
        }
    }
}

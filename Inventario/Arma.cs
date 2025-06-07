using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{

    public class Arma: Item
    {
        public int dano { get; set; }
        public int danoCritico { get; set; }

        public int probabilidadCritico { get; set; } = 20;
        public Arma(string nombre, string descripcion, int precio, int dano, int danoCritico, int probabilidadCritico)
            : base(nombre, descripcion, precio)
        {
            this.dano = dano;
            this.danoCritico = danoCritico;
            this.probabilidadCritico = probabilidadCritico;
        }
        public override string ToString()
        {
            return base.ToString() + $" Daño: {dano}, Daño crítico: {danoCritico}";
        }
    }
}

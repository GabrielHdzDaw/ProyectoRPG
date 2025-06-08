using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{

    public class Arma: Item
    {
        public int Dano { get; set; }
        public int DanoCritico { get; set; }

        public int ProbabilidadCritico { get; set; } = 20;
        public Arma(string nombre, string descripcion, int precio, int dano, int danoCritico, int probabilidadCritico)
            : base(nombre, descripcion, precio)
        {
            this.Dano = dano;
            this.DanoCritico = danoCritico;
            this.ProbabilidadCritico = probabilidadCritico;
        }
        public override string ToString()
        {
            return base.ToString() + $" Daño: {Dano}, Daño crítico: {DanoCritico}, Probabilidad de crítico: {ProbabilidadCritico}";
        }
    }
}

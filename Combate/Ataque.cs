using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Combate
{
    public class Ataque
    {
        public string nombre { get; }
        public int dano { get; }
        public int probabilidad { get; }

        public Ataque(string nombre, int dano, int probabilidad)
        {
            this.nombre = nombre;
            this.dano = dano;
            this.probabilidad = probabilidad;
        }

        public override string ToString()
        {
            return $"{nombre} (Daño: {dano}, Probabilidad: {probabilidad})";
        }
    }
}

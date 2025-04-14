using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Jugador:Personaje
    {
        string nombre { get; set; }
        string sprite { get; set; }
        int vida { get; set; }
        int ataque { get; set; }
        int defensa { get; set; }
        int velocidad { get; set; }

        public Jugador(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad) : base(nombre, sprite, vida, ataque, defensa, velocidad)
        { }

        public override string ToString()
        {
            return "Jugador " + base.ToString();
        }
    }
}

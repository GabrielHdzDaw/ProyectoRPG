using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Combate;

namespace ProyectoRPG.Personajes
{
    internal class Jugador:Personaje
    {
        string nombre { get; set; }
        string sprite { get; set; }
        int vida { get; set; }
        int ataque { get; set; }
        int defensa { get; set; }
        int velocidad { get; set; }
        public List<Ataque> Ataques { get; protected set; }


        public Jugador(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad) : base(nombre, sprite, vida, ataque, defensa, velocidad)
        {
            Ataques = new List<Ataque>();
        }
        
        public List<Ataque> GetAtaques()
        {
            return Ataques;
        }   

        public override string ToString()
        {
            return "Jugador " + base.ToString();
        }
    }
}

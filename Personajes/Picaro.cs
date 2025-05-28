using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Minijuegos;
using ProyectoRPG.Combate;
using ProyectoRPG.Recursos;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Personajes
{
    
    internal class Picaro:Jugador
    {
        Ahorcado minijuego = new Ahorcado(new List<string>() // Hacer txt con un puñao de palabras y cargar 10 de ellas random
        {
            "dragón", "castillo", "princesa", "caballero", "aventura", "tesoro", "magia", "espada", "escudo", "reino"
        });
        public Picaro(string nombre) 
            : base(nombre, Sprites.Picaro, 80, 25, 10, 12)
        {
            GetAtaques().Add(new Ataque("Ataque rápido", 10, 90));
            GetAtaques().Add(new Ataque("Ataque sigiloso", 17, 70));
            GetAtaques().Add(new Ataque("Ataque rabioso", 25, 35));

            AgregarItem(new Arma("Puñal barato", "Puñal endeble que podría romperse en cualquier momento", 0, 10, 17));
        }

        public Ahorcado GetMinijuego()
        {
            return minijuego;
        }


        public override string ToString()
        {
            return "Picaro " + base.ToString();
        }
    }
}

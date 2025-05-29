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
            : base(nombre, Sprites.Picaro, 75, 22, 8, 15)
        {
            GetAtaques().Add(new Ataque("Ataque rápido", 10, 90));
            GetAtaques().Add(new Ataque("Ataque sigiloso", 20, 70));
            GetAtaques().Add(new Ataque("Ataque rabioso", 30, 30));
            Arma arma = new Arma("Puñal barato", "Puñal endeble que podría romperse en cualquier momento", 0, 9, 25, 25);
            AgregarItem(arma);
            SetArma(arma);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Combate;
using ProyectoRPG.Inventario;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Personajes
{
    internal class Caballero: Jugador
    {

        public Caballero(string nombre) 
            : base(nombre, Sprites.Caballero, 100, 20, 15, 7)
        {
            GetAtaques().Add(new Ataque("Flecha rápida", 8, 95));
            GetAtaques().Add(new Ataque("Flecha pesada", 15, 80));
            GetAtaques().Add(new Ataque("Triple flecha", 25, 70));

            AgregarItem(new Arma("Espada corta rota", "Espada que ha visto mejores días", 0, 10, 15));

        }
        public override string ToString()
        {
            return "Caballero " + base.ToString();
        }
    }
    
}

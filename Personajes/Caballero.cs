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
    internal class Caballero : Jugador
    {

        public Caballero(string nombre)
    : base(nombre, Sprites.Caballero, 120, 15, 20, 5)
        {
            GetAtaques().Add(new Ataque("Ataque rápido", 8, 90));
            GetAtaques().Add(new Ataque("Ataque pesado", 18, 75));
            GetAtaques().Add(new Ataque("Ataque poderoso", 25, 50));
            Arma arma = new Arma("Espada corta rota", "Espada que ha visto mejores días", 0, 8, 12, 15);
            AgregarItem(arma);
            SetArma(arma);
        }
        public override string ToString()
        {
            return "Caballero " + base.ToString();
        }
    }

}

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

    public class Picaro:Jugador
    {

        public Picaro() { }
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


        public override string ToString()
        {
            return "Picaro " + base.ToString();
        }
    }
}

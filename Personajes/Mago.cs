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
    internal class Mago:Jugador
    {
        public Mago(string nombre) 
            : base(nombre, Sprites.Mago, 90, 30, 5, 10)
        {
            GetAtaques().Add(new Ataque("Bola de fuego", 10, 90));
            GetAtaques().Add(new Ataque("Rayo", 15, 80));
            GetAtaques().Add(new Ataque("Tormenta elemental", 50, 35));

            AgregarItem(new Arma("Vara mágica oxidada", "Vara con poderes mágicos. De mala calidad.", 0, 10, 20));
        }
        
        public override string ToString()
        {
            return "Mago " + base.ToString();
        }
    }
}

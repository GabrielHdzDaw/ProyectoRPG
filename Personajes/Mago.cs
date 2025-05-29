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
            : base(nombre, Sprites.Mago, 80, 35, 5, 10)
        {
            GetAtaques().Add(new Ataque("Bola de fuego", 10, 90));
            GetAtaques().Add(new Ataque("Rayo", 15, 80));
            GetAtaques().Add(new Ataque("Tormenta elemental", 40, 30));
            Arma arma = new Arma("Vara mágica oxidada", "Vara con poderes mágicos. De mala calidad.", 0, 12, 25, 10);
            AgregarItem(arma);
            SetArma(arma);
        }
        
        public override string ToString()
        {
            return "Mago " + base.ToString();
        }
    }
}

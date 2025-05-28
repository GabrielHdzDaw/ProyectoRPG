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
    internal class Elfo:Jugador
    {
        public Elfo(string nombre) 
            : base(nombre, Sprites.Elfo, 90, 15, 18, 15)
        {
            GetAtaques().Add(new Ataque("Ataque rápido", 10, 90));
            GetAtaques().Add(new Ataque("Ataque pesado", 20, 70));
            GetAtaques().Add(new Ataque("Ataque poderoso", 30, 40));

            AgregarItem(new Arma("Arco corto", "Arco que usan los elfos para el entrenamiento", 0, 8, 15));
        }
        public override string ToString()
        {
            return "Elfo " + base.ToString();
        }
    }
}

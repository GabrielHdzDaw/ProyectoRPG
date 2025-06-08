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
    public class Elfo:Jugador
    {
        public Elfo() { }

        public Elfo(string nombre)
    : base(nombre, Sprites.Elfo, 85, 18, 12, 18)
        {
            GetAtaques().Add(new Ataque("Flecha rápida", 10, 95));
            GetAtaques().Add(new Ataque("Flecha pesada", 18, 80));
            GetAtaques().Add(new Ataque("Triple flecha", 30, 60));
            Arma arma = new Arma("Arco corto", "Arco que usan los elfos en el entrenamiento", 0, 10, 18, 20);
            AgregarItem(arma);
            SetArma(arma);
        }
        public override string ToString()
        {
            return "Elfo " + base.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Combate
    {
        Jugador jugador;
        Enemigo enemigo;

        public Combate(Jugador jugador, Enemigo enemigo)
        {
            this.jugador = jugador;
            this.enemigo = enemigo;
        }
    }
}

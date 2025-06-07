using ProyectoRPG.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Combate
{
    internal class CombateAleatorio
    {
        Jugador jugador;
        Combate combate;
        Enemigo enemigo = GeneradorEnemigos.EnemigoAleatorio();
        public CombateAleatorio(Jugador jugador)
        {
            this.jugador = jugador;
            this.combate = new Combate(jugador, enemigo);
        }

        public bool IniciarCombate()
        {
            return combate.EmpezarCombate();
        }
    }
}

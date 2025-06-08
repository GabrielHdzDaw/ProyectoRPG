using ProyectoRPG.Interfaz;
using ProyectoRPG.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Sistema;

namespace ProyectoRPG.Combate
{
    internal class CombateAleatorio
    {
        Partida partida;
        Combate combate;
        Enemigo enemigo = GeneradorEnemigos.EnemigoAleatorio();
        public CombateAleatorio(Partida partida)
        {
            this.partida = partida;
            this.combate = new Combate(partida, enemigo);
        }

        public bool IniciarCombate()
        {
            Dibujar.LimpiarPantalla();
            return combate.EmpezarCombate();
        }
    }
}

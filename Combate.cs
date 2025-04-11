using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Combate
    {
        Jugador jugador { get; set; }
        Enemigo enemigo { get; set; }

        public Combate(Jugador jugador, Enemigo enemigo)
        {
            this.jugador = jugador;
            this.enemigo = enemigo;
        }

        public void DibujarInterfazCombate()
        {
            Dibujar.DibujarRectangulo(30, 52, 40, 10, '▓');
            Dibujar.DibujarRectangulo(70, 52, 60, 10, '▓');
            Console.ReadLine();
        }

        public void IniciarCombate()
        {

        }

        public override string ToString()
        {
            return "Combate entre " + jugador.ToString() + " y " + enemigo.ToString();
        }
    }
}

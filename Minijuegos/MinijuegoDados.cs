using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;

namespace ProyectoRPG.Minijuegos
{
    public class MinijuegoDados : Minijuego
    {
        public int TiempoAnimacion;
        public int PasosAnimacion;
        public Random Random;

        public MinijuegoDados()
        {
            TiempoAnimacion = 2000;
            PasosAnimacion = 10;
            Random = new Random();
        }

        public MinijuegoDados(int tiempoAnimacion, int pasosAnimacion)
        {
            TiempoAnimacion = tiempoAnimacion;
            PasosAnimacion = pasosAnimacion;
            Random = new Random();
        }

        public bool Jugar()
        {
            int dadoJugador, dadoMaquina;

            do
            {
                int delay = TiempoAnimacion / PasosAnimacion;
                int mostrarJugador = 0, mostrarMaquina = 0;

                for (int i = 0; i < PasosAnimacion; i++)
                {
                    mostrarJugador = Random.Next(1, 7);
                    mostrarMaquina = Random.Next(1, 7);

                    Dibujar.LimpiarPantallaSimple();

                    MostrarDadosCentrados(mostrarJugador, mostrarMaquina);

                    Thread.Sleep(delay);
                }

                dadoJugador = mostrarJugador;
                dadoMaquina = mostrarMaquina;

                if (dadoJugador == dadoMaquina)
                {
                    Thread.Sleep(2000);
                    Dibujar.LimpiarPantallaSimple();
                }

            } while (dadoJugador == dadoMaquina);

            bool ganaste;
            if (dadoJugador > dadoMaquina)
            {
                ganaste = true;
            }
            else
            {
                ganaste = false;
            }

            Thread.Sleep(5000);

            return ganaste;
        }

        public void MostrarDadosCentrados(int valorJugador, int valorMaquina)
        {
            string[][] dados =
            {
                new string[]{"+-------+","|       |","|   *   |","|       |","+-------+"},
                new string[]{"+-------+","| *     |","|       |","|     * |","+-------+"},
                new string[]{"+-------+","| *     |","|   *   |","|     * |","+-------+"},
                new string[]{"+-------+","| *   * |","|       |","| *   * |","+-------+"},
                new string[]{"+-------+","| *   * |","|   *   |","| *   * |","+-------+"},
                new string[]{"+-------+","| *   * |","| *   * |","| *   * |","+-------+"}
            };

            string[] jugador = dados[valorJugador - 1];
            string[] maquina = dados[valorMaquina - 1];

            int ancho = Console.WindowWidth;
            int alto = Console.WindowHeight;

            int filaInicio = (alto / 2) - 4;
            int colInicio = (ancho / 2) - 16;

            // Etiquetas "Tú" y "Rival"
            Console.SetCursorPosition(colInicio + 3, filaInicio);
            Console.Write("Tú");
            Console.SetCursorPosition(colInicio + 16, filaInicio);
            Console.Write("Rival");

            for (int i = 0; i < jugador.Length; i++)
            {
                Console.SetCursorPosition(colInicio, filaInicio + i + 1);
                Console.CursorVisible = false;
                Console.Write(jugador[i] + "     " + maquina[i]);
            }

            Console.SetCursorPosition(0, filaInicio + jugador.Length + 2);
        }
    }
}

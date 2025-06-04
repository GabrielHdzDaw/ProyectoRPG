using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Minijuegos
{
    internal class MinijuegoDados : Minijuego
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

        public void Jugar()
        {
            int dadoJugador, dadoMaquina;

            Console.Clear();
            Console.WriteLine("¡Comienza la batalla!");
            Console.WriteLine("Presiona una tecla para tirar los dados...");

            do
            {
                Console.ReadKey(true);
                Console.Write("\nTirando los dados");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000);
                    Console.Write(" .");
                }

                Console.WriteLine();

                int delay = TiempoAnimacion / PasosAnimacion;
                int mostrarJugador = 0, mostrarMaquina = 0;

                for (int i = 0; i < PasosAnimacion; i++)
                {
                    mostrarJugador = Random.Next(1, 7);
                    mostrarMaquina = Random.Next(1, 7);

                    Console.Clear();
                    Console.WriteLine("¡Comienza la batalla!");
                    Console.WriteLine("\nDados girando...");

                    MostrarDadosCentrados(mostrarJugador, mostrarMaquina);

                    Thread.Sleep(delay);
                }

                dadoJugador = mostrarJugador;
                dadoMaquina = mostrarMaquina;

                Console.WriteLine($"\nTú sacaste: {dadoJugador}");
                Console.WriteLine($"El enemigo sacó: {dadoMaquina}");

                if (dadoJugador == dadoMaquina)
                {
                    Console.WriteLine("Empate, ¡se repite la tirada!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }

            } while (dadoJugador == dadoMaquina);

            if (dadoJugador > dadoMaquina)
            {
                Console.WriteLine("Has derrotado al enemigo");
                PuntuacionMinijuego += 50;
            }
            else
            { 
                Console.WriteLine("El enemigo ganó la batalla");
                PuntuacionMinijuego += 10;
            }
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

            int filaInicio = (alto / 2) - 3;
            int colInicio = (ancho / 2) - 16;

            for (int i = 0; i < jugador.Length; i++)
            {
                Console.SetCursorPosition(colInicio, filaInicio + i);
                Console.Write(jugador[i] + "     " + maquina[i]);
            }

            Console.SetCursorPosition(0, filaInicio + 6);
        }
    }
}

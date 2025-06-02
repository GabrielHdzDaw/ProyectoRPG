using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Minijuegos
{
    internal class MinijuegoPeleaMagos : Minijuego
    {
        public const int maxProgreso = 30;
        public int progresoJugador;
        public int progresoRival;
        public DateTime ultimoAvanceRival;
        public TimeSpan intervaloRival;
        public ConsoleKey teclaObjetivo;
        public Random random;

        public MinijuegoPeleaMagos()
        {
            progresoJugador = 0;
            progresoRival = 0;
            intervaloRival = TimeSpan.FromMilliseconds(660);
            ultimoAvanceRival = DateTime.Now;
            random = new Random();
            teclaObjetivo = GenerarTeclaAleatoria();
        }

        public void Jugar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("¡Presiona la tecla que aparece en pantalla!");
            Console.WriteLine("-------------------------------------------");

            MostrarBarras();
            MostrarFlecha();

            while (progresoJugador < maxProgreso && progresoRival < maxProgreso)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);
                    if (tecla.Key == teclaObjetivo)
                    {
                        progresoJugador++;
                        MostrarBarras();
                        teclaObjetivo = GenerarTeclaAleatoria();
                        MostrarFlecha();
                    }
                }

                if (DateTime.Now - ultimoAvanceRival >= intervaloRival)
                {
                    progresoRival++;
                    MostrarBarras();
                    ultimoAvanceRival = DateTime.Now;
                }

                Thread.Sleep(10);
            }

            Console.SetCursorPosition(0, 6);
            if (progresoJugador >= maxProgreso)
                Console.WriteLine("Has derrotado al enemigo.");
            else
                Console.WriteLine("El rival te derrotó.");

            Console.ResetColor();
        }

        public ConsoleKey GenerarTeclaAleatoria()
        {
            ConsoleKey[] teclas = { ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
            return teclas[random.Next(teclas.Length)];
        }

        public void MostrarFlecha()
        {
            Console.SetCursorPosition(0, 4);
            Console.Write("Presiona: ");
            switch (teclaObjetivo)
            {
                case ConsoleKey.LeftArrow:
                    Console.WriteLine("Izquierda");
                    break;
                case ConsoleKey.RightArrow:
                    Console.WriteLine("Derecha  ");
                    break;
            }
        }

        public void MostrarBarras()
        {
            Console.SetCursorPosition(0, 2);
            Console.Write("Jugador: [");
            Console.Write(new string('█', progresoJugador));
            Console.Write(new string(' ', maxProgreso - progresoJugador));
            Console.WriteLine("]");

            Console.Write("Rival:   [");
            Console.Write(new string('█', progresoRival));
            Console.Write(new string(' ', maxProgreso - progresoRival));
            Console.WriteLine("]");
        }
    }
}

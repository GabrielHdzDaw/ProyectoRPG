using System;
using System.Threading;
using ProyectoRPG.Interfaz;

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

        public bool Jugar()
        {
            Dibujar.LimpiarPantallaSimple();
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

            int centroY = Console.WindowHeight / 2 + 4;
            Console.SetCursorPosition(0, centroY);
            bool ganaste = progresoJugador >= maxProgreso;

            if (ganaste)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 32);
                Console.WriteLine("¡Enhorabuena, has ganado al mago!".PadRight(60));
                Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 30);
                Console.ResetColor();
                Console.WriteLine("Pulsa cualquier tecla para continuar...".PadRight(60));
                Console.CursorVisible = false;
                Console.ReadKey(true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 32);
                Console.WriteLine("¡Buen intento pero el mago te ha ganado!".PadRight(60));
                Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 30);
                Console.ResetColor();
                Console.WriteLine("Pulsa cualquier tecla para continuar...".PadRight(60));
                Console.CursorVisible = false;
                Console.ReadKey(true);
            }

            Console.Clear();
            Console.ResetColor();
            Dibujar.DibujarRectanguloPrincipal();
            return ganaste;
        }

        public ConsoleKey GenerarTeclaAleatoria()
        {
            ConsoleKey[] teclas = { ConsoleKey.Q, ConsoleKey.E };
            return teclas[random.Next(teclas.Length)];
        }

        public void MostrarFlecha()
        {
            int centroX = Console.WindowWidth / 2;
            int y = Console.WindowHeight / 2;

            Console.SetCursorPosition(0, y);
            ImprimirCentrado("Presiona: " + (teclaObjetivo == ConsoleKey.Q ? "Q" : "E"));
        }

        public void MostrarBarras()
        {
            Console.CursorVisible = false;
            int centroY = Console.WindowHeight / 2 - 2;
            Console.SetCursorPosition(0, centroY);
            ImprimirCentrado($"Jugador: [{new string('█', progresoJugador)}{new string(' ', maxProgreso - progresoJugador)}]");

            Console.SetCursorPosition(0, centroY + 1);
            ImprimirCentrado($"Rival:   [{new string('█', progresoRival)}{new string(' ', maxProgreso - progresoRival)}]");
        }

        private void ImprimirCentrado(string texto)
        {
            int centroX = (Console.WindowWidth - texto.Length) / 2;
            Console.SetCursorPosition(centroX < 0 ? 0 : centroX, Console.CursorTop);
            Console.WriteLine(texto);
        }
    }
}

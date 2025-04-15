using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal abstract class MenuPrincipal
    {
        public static int Menu()
        {
            Console.CursorVisible = false;

            int centroX = Dibujar.X + (Dibujar.AnchuraRectangulo / 2) - 1;
            int centroY = Dibujar.Y + (Dibujar.AlturaRectangulo / 2) - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar", "Nueva partida", "Records", "Créditos", "Salir"];
            int opcion = 0;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            
            // Repetir este bucle hasta que se pulse enter
            while (tecla.Key != ConsoleKey.Enter)
            {
                int espaciadoVertical = 0;
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, opciones[i]);
                    espaciadoVertical -= 1;

                    if (opcion == i)
                    {
                        Console.ResetColor();
                    }
                }

                if (Console.KeyAvailable)
                {
                    tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (opcion - 1 >= 0)
                                opcion--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (opcion + 1 < opciones.Length)
                                opcion++;
                            break;
                    }
                }
            }

            Console.CursorVisible = true;

            return opcion;
        }

        public static int OpcionSeleccionada(int opcion)
        {
            int resultado = opcion;
            switch(opcion)
            {
                case 0: // Continuar
                    break;
                case 1:
                    Partida partida = Partida.NuevaPartida();
                    // ···
                    break;
                case 2:
                    Records();
                    break;
                case 3:
                    Creditos();
                    break;
                case 4:
                    resultado = Salir();
                    break;
            }

            return resultado;
        }

        private static void Records()
        {
            // Por hacer
        }

        private static void Creditos()
        {
            // Por hacer
        }

        private static int Salir()
        {
            Dibujar.LimpiarPantalla();
            Console.CursorVisible = false;

            int centroX = Dibujar.X + (Dibujar.AnchuraRectangulo / 2) - 1;
            int centroY = Dibujar.Y + (Dibujar.AlturaRectangulo / 2) - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");
            
            Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - 0, "¿Seguro que deseas salir sin ver a los creadores? Son muy majos.");

            string[] opciones = ["SÍ", "NO"];
            int indice = 0;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            // Repetir este bucle hasta que se pulse enter
            while (tecla.Key != ConsoleKey.Enter)
            {
                int espaciadoVertical = -2;
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (indice == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, opciones[i]);
                    espaciadoVertical -= 1;

                    if (indice == i)
                    {
                        Console.ResetColor();
                    }
                }

                if (Console.KeyAvailable)
                {
                    tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (indice - 1 >= 0)
                                indice--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (indice + 1 < opciones.Length)
                                indice++;
                            break;
                    }
                }
            }

            if(indice == 0)
            {
                Console.Clear();
            }
            else
            {
                Dibujar.LimpiarPantallaSimple();
            }

            Console.CursorVisible = true;

            return indice == 0 ? 4 : 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Combate
{
    internal class PruebaCombate
    {
        const int ANCHURA_RECTANGULO = 176; // Dentro el ancho es de 175
        const int ALTURA_RECTANGULO = 44; // Dentro el ancho es de 43

        // Ajustes para la pantalla
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern nint GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(nint hWnd, int nCmdShow);

        const int SW_MAXIMIZE = 3;


        public static void PrepararVentanaInicio()
        {
            Console.Title = "RPG";
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            nint consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MAXIMIZE);
        }

        public static int Menu(int x, int y, int maxAnchura, int maxAltura) // [ IMPORTANTE ] Versión gráfica y lógica del menú, tocará cambiarla de sitio y demás
        {
            Console.CursorVisible = false;

            int centroX = x + maxAnchura / 2 - 1;
            int centroY = y + maxAltura / 2 - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar", "Nueva partida", "Records", "Creadores", "Salir"];
            int opcion = 0;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

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

                tecla = Console.ReadKey();

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
            Console.CursorVisible = true;

            return opcion;
        }
        //public static void Main(string[] args)
        //{
        //    Console.CursorVisible  = false;
        //    PrepararVentanaInicio();
        //    Dibujar.DibujarRectanguloPrincipal();

        //    Caballero jugador = new Caballero("Caballero");
        //    Enemigo enemigo = new Enemigo("Centauro", Sprites.Centauro, 100, 18, 12, 3);

        //    Combate combate = new Combate(jugador, enemigo);

        //    combate.DibujarInterfazCombate();
        //    combate.EmpezarCombate();
        //}
    }
}

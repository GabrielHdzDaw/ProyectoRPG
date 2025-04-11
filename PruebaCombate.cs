using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class PruebaCombate
    {
        const int ANCHURA_RECTANGULO = 176; // Dentro el ancho es de 175
        const int ALTURA_RECTANGULO = 44; // Dentro el ancho es de 43

        // Ajustes para la pantalla
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_MAXIMIZE = 3;


        public static void PrepararVentanaInicio()
        {
            Console.Title = "RPG";
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            IntPtr consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MAXIMIZE);
        }

        public static int Menu(int x, int y, int maxAnchura, int maxAltura) // [ IMPORTANTE ] Versión gráfica y lógica del menú, tocará cambiarla de sitio y demás
        {
            Console.CursorVisible = false;

            int centroX = x + (maxAnchura / 2) - 1;
            int centroY = y + (maxAltura / 2) - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar", "Nueva partida", "Records", "Creadores", "Salir"];
            int opcion = 0;

            // Repetir este for hasta que se pulse enter
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
        //    PrepararVentanaInicio();

        //    int x = (Console.WindowWidth - ANCHURA_RECTANGULO) / 2 + 1;
        //    int y = (Console.WindowHeight - ALTURA_RECTANGULO) / 2 + 1;
            

        //    Dibujar.DibujarRectangulo(x - 1, y - 1, ANCHURA_RECTANGULO, ALTURA_RECTANGULO, '▓');

        //    //Dibujar.Inicio(x, y, ANCHURA_RECTANGULO, ALTURA_RECTANGULO);

        //    //int opcion = 0;
        //    //while (opcion != 4)
        //    //{
        //    //    opcion = Menu(x, y, ANCHURA_RECTANGULO, ALTURA_RECTANGULO);
                
        //    //}

        //    Jugador jugador = new Jugador("Jugador", "o.o", 100, 20, 20, 10, 0, false);
        //    Enemigo enemigo = new Enemigo("Enemigo", "ò.ó", 100, 15, 8, 3);

        //    Combate combate = new Combate(jugador, enemigo);
        //    combate.DibujarInterfazCombate();
        //}
    }
}

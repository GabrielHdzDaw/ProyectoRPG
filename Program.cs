/* AUTORES:
 * 
 * Gabriel Hernandez Collado
 * Lucía Navarro Cruz
 * Álvaro Martí Cerdán
 * Dayron Alexis Lucero Cortez
 * Carlos Rodrigo Beltrá
 */

using System;
using System.Runtime.InteropServices;


namespace ProyectoRPG
{
    internal class Program
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

        public static void Main(string[] args)
        {
            PrepararVentanaInicio();

            int x = (Console.WindowWidth - ANCHURA_RECTANGULO) / 2 + 1;
            int y = (Console.WindowHeight - ALTURA_RECTANGULO) / 2 + 1;

            Dibujar.DibujarRectangulo(x - 1, y - 1, ANCHURA_RECTANGULO, ALTURA_RECTANGULO, '▓');

            Dibujar.Inicio(x, y, ANCHURA_RECTANGULO, ALTURA_RECTANGULO);

            int opcion = 0;
            while (opcion != 4)
            {
                opcion = MenuPrincipal.Menu(x, y, ANCHURA_RECTANGULO, ALTURA_RECTANGULO);
                opcion = MenuPrincipal.Salir(x, y, ANCHURA_RECTANGULO, ALTURA_RECTANGULO, opcion);
            }

            Console.Clear();
            Thread.Sleep(500);
        }
    }
}

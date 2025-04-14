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

            Dibujar.DibujarRectanguloPrincipal();

            Dibujar.Inicio();

            int opcion = 0;
            while (opcion != 4)
            {
                opcion = MenuPrincipal.Menu();
                opcion = MenuPrincipal.OpcionSeleccionada(opcion);
            }
        }
    }
}

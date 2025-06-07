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
using ProyectoRPG.Interfaz;


namespace ProyectoRPG.Sistema
{
    public class Program
    {
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

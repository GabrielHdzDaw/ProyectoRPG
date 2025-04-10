/* Autores: 
/* 
*/
using System;
using System.Runtime.InteropServices;


namespace ProyectoRPG
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_MAXIMIZE = 3;
        public static void Main(string[] args)
        {

            //Maximizar la ventana al incio
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            IntPtr consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MAXIMIZE);

            //Todo el juego se mostrará dentro de 174 - 70

            int anchuraRectangulo = 175;
            int alturaRectangulo = 71;

            int x = (Console.WindowWidth - anchuraRectangulo) / 2;
            int y = (Console.WindowHeight - alturaRectangulo) / 2;

            Dibujar.DibujarRectangulo(x, y, anchuraRectangulo, alturaRectangulo, '▓');

        }
    }
}

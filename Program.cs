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
            const int ANCHURA_RECTANGULO = 175;
            const int ALTURA_RECTANGULO = 61;
            int x_marco = (Console.WindowWidth - ANCHURA_RECTANGULO) / 2;
            int y_marco = (Console.WindowHeight - ALTURA_RECTANGULO) / 2;

            Dibujar.DibujarRectangulo(x_marco, y_marco, ANCHURA_RECTANGULO, ALTURA_RECTANGULO, '▓');

            Texto texto = new Texto(Console.WindowWidth / 2, Console.WindowHeight / 2, 15, "Me cago en la puta");
            texto.DrawTextCentered();
            Console.ReadLine();
        }
    }
}

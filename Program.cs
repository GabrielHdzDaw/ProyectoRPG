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
        static void Main(string[] args)
        {
            //Maximizar la ventana al incio
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            IntPtr consoleWindow = GetConsoleWindow();
            ShowWindow(consoleWindow, SW_MAXIMIZE);


            Dibujar.DibujarRectangulo(1,1, 28, 28, 'o');
        }
    }
}

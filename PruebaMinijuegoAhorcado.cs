using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class PruebaMinijuegoAhorcado
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

        //public static void Main(string[] args)
        //{
        //    PrepararVentanaInicio();
        //    List<string> palabras = new List<string> ();
        //    palabras.Add("tomate");
        //    palabras.Add("ballena");
        //    palabras.Add("berenjena");
        //    palabras.Add("hipopotamo");
        //    palabras.Add("sandia");
        //    palabras.Add("lechuga");

        //    Ahorcado minijuego = new Ahorcado(palabras);
        //    minijuego.Jugar();
        //}
    }
}

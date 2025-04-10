/* AUTORES:
 * 
 * [ IMPORTANTE ] Poner los apellidos
 * 
 * Gabriel
 * Lucía
 * Álvaro
 * Dayron
 * Carlos
 */

using System;
using System.Runtime.InteropServices;


namespace ProyectoRPG
{
    internal class Program
    {
        public const int ANCHURA_RECTANGULO = 176; // Dentro el ancho es de 175
        public const int ALTURA_RECTANGULO = 44; // Dentro el ancho es de 43

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

        public static int Menu(int x, int y) // [ IMPORTANTE ] Versión gráfica y lógica del menú, tocará cambiarla de sitio y demás
        {
            Console.CursorVisible = false;

            int centroX = x + (ANCHURA_RECTANGULO / 2) - 1;
            int centroY = y + (ALTURA_RECTANGULO / 2) - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar","Nueva partida","Records","Créditos","Salir"];
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

                if(Console.KeyAvailable)
                {
                    tecla = Console.ReadKey();

                    switch(tecla.Key)
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

        public static void Main(string[] args)
        {
            PrepararVentanaInicio();

            int x = (Console.WindowWidth - ANCHURA_RECTANGULO) / 2 + 1;
            int y = (Console.WindowHeight - ALTURA_RECTANGULO) / 2 + 1;

            Dibujar.DibujarRectangulo(x - 1, y - 1, '▓');

            Dibujar.Inicio(x, y);

            int opcion = 0;
            while (opcion != 4)
            {
                opcion = Menu(x, y);
                // Arranca todo aquí
            }
        }
    }
}

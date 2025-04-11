using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal abstract class MenuPrincipal
    {
        public static int Menu(int x, int y, int maxAnchura, int maxAltura) // [ IMPORTANTE ] Versión gráfica y lógica del menú, tocará cambiarla de sitio y demás
        {
            Console.CursorVisible = false;


            int centroX = x + (maxAnchura / 2) - 1;
            int centroY = y + (maxAltura / 2) - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar", "Nueva partida", "Records", "Créditos", "Salir"];
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

                if (Console.KeyAvailable)
                {
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
            }

            Console.CursorVisible = true;

            return opcion;
        }
        public static int Salir(int x, int y, int maxAnchura, int maxAltura, int opcion)
        {            
            if (opcion == 4)
            {
                Dibujar.LimpiarPantalla(x, y, maxAnchura - 1, maxAltura + y - 1);
                Console.CursorVisible = false;


                int centroX = x + (maxAnchura / 2) - 1;
                int centroY = y + (maxAltura / 2) - 1;

                Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                    "\r\n█▀▄ █▀▀ █▄█");

                string[] opciones = ["SÍ", "NO"];
                int indice = 0;

                // Repetir este for hasta que se pulse enter
                ConsoleKeyInfo tecla = new ConsoleKeyInfo();

                while (tecla.Key != ConsoleKey.Enter)
                {
                    int espaciadoVertical = 0;
                    for (int i = 0; i < opciones.Length; i++)
                    {
                        if (indice == i)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, opciones[i]);
                        espaciadoVertical -= 1;

                        if (indice == i)
                        {
                            Console.ResetColor();
                        }
                    }

                    if (Console.KeyAvailable)
                    {
                        tecla = Console.ReadKey();

                        switch (tecla.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (indice - 1 >= 0)
                                    indice--;
                                break;
                            case ConsoleKey.DownArrow:
                                if (indice + 1 < opciones.Length)
                                    indice++;
                                break;
                        }
                    }
                }

                Console.CursorVisible = true;

                opcion = indice == 0 ? 4 : 0;
            }
            return opcion;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Inventario;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Sistema
{
    public abstract class MenuPrincipal
    {
        public static int Menu()
        {
            Console.CursorVisible = false;

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            string[] opciones = ["Continuar", "Nueva partida", "Records", "Créditos", "Salir"];
            int opcion = 0;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            
            // Repetir este bucle hasta que se pulse enter
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
                    tecla = Console.ReadKey(true);

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

        public static int OpcionSeleccionada(int opcion)
        {
            int resultado = opcion;
            switch (opcion)
            {
                case 0:
                    Partida partidaEmpezada = Partida.RenaudarPartida();
                    if (partidaEmpezada != null)
                    {
                        Dibujar.LimpiarPantalla();
                        partidaEmpezada.Continuar(partidaEmpezada);
                    }
                    else
                    {
                        Dibujar.LimpiarPantalla();
                    }
                    break;
                case 1:
                    Partida partida = Partida.NuevaPartida();
                    partida.Continuar(partida);
                    break;
                case 2:
                    Records();
                    break;
                case 3:
                    Creditos();
                    break;
                case 4:
                    resultado = Salir();
                    break;
            }

            return resultado;
        }

        private static void Records()
        {
            Dibujar.LimpiarPantalla();
            List<Partida> partidas = Partida.CargarPartidas();

            List<Partida> partidasAcabadas = partidas.FindAll(p => p.terminada);
            
            partidasAcabadas.Sort();

            Console.CursorVisible = false;

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;

            int opcion = 0;
            bool salir = false;

            Dibujar.DibujarSpriteCentrado(centroX - 55, centroY - 15, "Récords: ");

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            while (!salir)
            {
                int espaciadoVertical = 10;
                int maxPartidasMostrar = Math.Min(10, partidasAcabadas.Count());
                int maxMostrar = maxPartidasMostrar + 1;

                for (int i = 0; i < maxMostrar; i++)
                {
                    string simb = "\u2192";
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    if (i < maxPartidasMostrar)
                    {
                        Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, $"{i + 1}. " + partidasAcabadas[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical + 4, simb + " Salir");
                        Console.ResetColor();
                    }

                    espaciadoVertical -= 2;

                    if (opcion == i)
                    {
                        Console.ResetColor();
                    }
                }

                if (Console.KeyAvailable)
                {
                    tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (opcion > 0)
                                opcion--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (opcion < maxMostrar - 1)
                                opcion++;
                            break;
                        case ConsoleKey.Enter:
                            if (opcion == maxMostrar -1)
                            {
                                salir = true;
                            }
                            break;
                    }
                }
            }
            Dibujar.LimpiarPantalla();

        }

        private static void Creditos()
        {
            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;
            string cora = "\u2665";

            Dibujar.LimpiarPantalla();
            Console.ForegroundColor = ConsoleColor.Green;
            Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - 0, $"\"Nos alegramos muchísimoooo de que quieras conocer a los creadores\"");
            Console.ResetColor();
            Dibujar.DibujarSpriteCentrado(centroX + 34, centroY - 0, cora);
            Dibujar.DibujarSpriteCentrado(centroX - 1, centroY + 2, "Pulse ENTER para conocerlos...");
            bool avanzar = false;
            while (!avanzar)
            {
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    avanzar = Console.ReadKey(true).Key == ConsoleKey.Enter;
                }
            }
            Dibujar.LimpiarPantalla();
            VerCreadores();
            Dibujar.LimpiarPantalla();
            Dibujar.DibujarRectanguloPrincipal();
        }

        private static void VerCreadores()
        {
            string[] opciones = ["Álvaro", "Dayron", "Gabriel", "Lucía", "Carlos", "Salir"];
            string[] textosPersonajes = ["Un chico muy listo que con perilla es más guapo aún...",
                                         "¡Gracias por jugar!, espero poder disfrutar del juego ahora que ya he terminado todo",
                                         "El padre de la clase, palabras textuales de \"Don José Manuel Fuster\"",
                                         "Ha sido un placer para mí poder trabajar en este juego. Espero que os haya gustado :)",
                                         "Un chico que cuando sabe lo que le gusta, no se lo piensa dos veces...",
                                         ""];
            int indice = 0;

            int xSprite = (int)(Dibujar.AnchuraRectangulo / 1.40) - 5;
            int ySprite = Dibujar.Y + Dibujar.AlturaRectangulo / 4 - 5;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            bool salir = false;
            while (!salir)
            {
                Console.CursorVisible = false;

                int espaciadoVertical = 1;
                Dibujar.DibujarRectanguloPrincipal();
                Dibujar.DibujarRectangulo(Dibujar.X, Dibujar.Y, Dibujar.AlturaRectangulo, Dibujar.AnchuraRectangulo / 4, Dibujar.Caracter);

                for (int i = 0; i < opciones.Length; i++)
                {
                    string simb = "\u2192";
                    Console.CursorVisible = false;
                    if (indice == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    if (i == opciones.Length - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Dibujar.DibujarSpriteCentrado(Dibujar.X + Dibujar.AnchuraRectangulo / 4 / 2, Dibujar.Y + Dibujar.AlturaRectangulo / 5 - espaciadoVertical, simb + " " + opciones[i]);
                    }
                    else
                    {
                        Dibujar.DibujarSpriteCentrado(Dibujar.X + Dibujar.AnchuraRectangulo / 4 / 2, Dibujar.Y + Dibujar.AlturaRectangulo / 5 - espaciadoVertical, opciones[i]);
                    }
                    Console.ResetColor();
                    espaciadoVertical -= 6;

                    if (indice == i)
                    {
                        Console.ResetColor();
                    }

                    switch (indice)
                    {
                        case 0:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 -8, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2 -2, Sprites.Alvaro); 
                            break;
                        case 1:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 6, Dibujar.Y + 2 + Dibujar.AlturaRectangulo / 4 / 2 -4, Sprites.Dayron); 
                            break;
                        case 2:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 + 2, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2 - 2, Sprites.Gabriel);
                            break;
                        case 3:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 5, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2 - 2, Sprites.Lucia); 
                            break;
                        case 4:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 10, Dibujar.Y + Dibujar.AlturaRectangulo / 4 - 8, Sprites.Carlos);
                            break;
                        case 5:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 -5, Dibujar.Y + Dibujar.AlturaRectangulo / 4 + 4, Sprites.Gracias);
                            break;
                    }

                    Dibujar.DibujarSpriteCentrado(xSprite + (Dibujar.X - 14), Dibujar.AlturaRectangulo + Dibujar.Y - 5, textosPersonajes[indice]);
                }

                if (Console.KeyAvailable)
                {
                    tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (indice - 1 >= 0)
                            {
                                indice--;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (indice + 1 < opciones.Length)
                            {
                                indice++;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.Enter:
                            if (indice == 5)
                            {
                                salir = true;
                            }
                            break;
                    }
                }
            }

            Console.CursorVisible = true;
        }

        private static int Salir()
        {
            Dibujar.LimpiarPantalla();
            Console.CursorVisible = false;

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;

            Dibujar.DibujarSpriteCentrado(centroX, centroY - 4, "\r\n█▀█ █▀█ █▀▀" +
                                                                "\r\n█▀▄ █▀▀ █▄█");

            Console.ForegroundColor = ConsoleColor.Red;
            Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - 0, "¿Seguro que deseas salir sin ver a los creadores? Son muy majos.");
            Console.ResetColor();

            string[] opciones = ["SÍ", "NO"];
            int indice = 0;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            // Repetir este bucle hasta que se pulse enter
            while (tecla.Key != ConsoleKey.Enter)
            {
                int espaciadoVertical = -2;
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
                    tecla = Console.ReadKey(true);

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

            if(indice == 0)
            {
                Console.Clear();
            }
            else
            {
                Dibujar.LimpiarPantallaSimple();
            }

            Console.CursorVisible = true;

            return indice == 0 ? 4 : 0;
        }
    }
}

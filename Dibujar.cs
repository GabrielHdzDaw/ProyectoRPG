using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Dibujar
    {
        static int anchuraRectangulo = 176; // Dentro el ancho es de 175
        static int alturaRectangulo = 44; // Dentro el ancho es de 43
        static int x = (Console.WindowWidth - AnchuraRectangulo) / 2 + 1;
        static int y = (Console.WindowHeight - alturaRectangulo) / 2 + 1;
        static char caracter = '▓';

        public static int AnchuraRectangulo { get => anchuraRectangulo; }
        public static int AlturaRectangulo { get => alturaRectangulo; }
        public static int X { get => x; }
        public static int Y { get => y; }
        public static char Caracter { get => caracter; }

        private static int GetAnchuraDibujo(string dibujo)
        {
            string[] lineas = dibujo.Split('\n');
            int anchuraMaxima = 0;
            foreach (string linea in lineas)
            {
                if (linea.Length > anchuraMaxima)
                    anchuraMaxima = linea.Length;
            }
            return anchuraMaxima;
        }

        public static void DibujarSpriteNormal(int posicionX, int posicionY, string dibujo)
        {
            int anchuraDibujo = GetAnchuraDibujo(dibujo);

            string[] lineas = dibujo.Split('\n');
            for (int i = 0; i < lineas.Length; i++)
            {
                Console.SetCursorPosition(posicionX, posicionY + i);

                if (i == lineas.Length - 1)
                {
                    Console.Write(lineas[i] + "\n");
                }
                else
                {
                    Console.Write(lineas[i]);
                }
            }
        }

        public static void DibujarSpriteCentrado(int posicionX, int posicionY, string dibujo)
        {
            int anchuraDibujo = GetAnchuraDibujo(dibujo);
            int comienzoX = posicionX - (anchuraDibujo / 2);

            string[] lineas = dibujo.Split('\n');
            for (int i = 0; i < lineas.Length; i++)
            {
                Console.SetCursorPosition(comienzoX, posicionY + i);

                if (i == lineas.Length - 1)
                {
                    Console.Write(lineas[i] + "\n");
                }
                else
                {
                    Console.Write(lineas[i]);
                }
            }
        }

        public static void DibujarRectanguloPrincipal()
        {
            for (int i = 0; i <= alturaRectangulo; i++)
            {
                for (int j = 0; j <= anchuraRectangulo; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    if (i == 0 || i == alturaRectangulo || j == 0 || j == anchuraRectangulo)
                    {
                        if (j == anchuraRectangulo || i == alturaRectangulo)
                        {
                            Console.Write(caracter);
                        }
                        else
                        {
                            Console.Write(caracter);
                        }
                    }
                }
            }
        }

        public static void DibujarRectangulo(int posicionX, int posicionY, int anchuraMax, int alturaMax, char caracter)
        {
            for (int i = 0; i <= alturaMax; i++)
            {
                for (int j = 0; j <= anchuraMax; j++)
                {
                    Console.SetCursorPosition(posicionX + j, posicionY + i);
                    if (i == 0 || i == alturaMax || j == 0 || j == anchuraMax)
                    {
                        if (j == anchuraMax || i == alturaMax)
                        {
                            Console.Write(caracter);
                        }
                    }
                }
            }
        }

        public static void LimpiarPantallaSimple()
        {
            for (int i = x+1; i < x + anchuraRectangulo; i++)
            {
                for (int j = y+1; j < alturaRectangulo + y; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }

        public static void LimpiarPantalla()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            LimpiarPantallaSimple();
            Console.ResetColor();
            LimpiarPantallaSimple();
        }

        public static int ContarSaltos(string cadena)
        {
            int resultado = 0;

            if (cadena.Contains('\n'))
            {
                foreach (char c in cadena)
                {
                    if(c == '\n')
                    {
                        resultado++;
                    }
                }
            }

            return resultado;
        }

        public static void EscribirTexto(string cadena)
        {
            foreach (char c in cadena)
            {
                if (c == '.')
                {
                    Thread.Sleep(250);
                }

                Console.Write(c);

                if (c == '.')
                {
                    Thread.Sleep(250);
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }

        public static void Inicio()
        {
            int inicioX = x + (anchuraRectangulo / 2);
            int inicioY = y + (alturaRectangulo / 2);

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Dibujar.DibujarSpriteCentrado(inicioX, inicioY - 3, "¡ ADVERTENCIA !");
            Console.ResetColor();

            Dibujar.DibujarSpriteCentrado(inicioX, inicioY - 1, "Se recomienda poner la pantalla completa (F11) para mejor expriencia");
            Dibujar.DibujarSpriteCentrado(inicioX, inicioY + 1, "Para continuar pulse ENTER...");

            bool pantallaCompleta = false;
            while (!pantallaCompleta)
            {
                if (Console.KeyAvailable)
                {
                    pantallaCompleta = Console.ReadKey().Key == ConsoleKey.Enter;
                }
            }
            Console.CursorVisible = true;

            Dibujar.LimpiarPantalla();

            string titulo = "\r\n██████╗░██████╗░░██████╗░" +
                            "\r\n██╔══██╗██╔══██╗██╔════╝░" +
                            "\r\n██████╔╝██████╔╝██║░░██╗░" +
                            "\r\n██╔══██╗██╔═══╝░██║░░╚██╗" +
                            "\r\n██║░░██║██║░░░░░╚██████╔╝" +
                            "\r\n╚═╝░░╚═╝╚═╝░░░░░░╚═════╝░";
            Dibujar.DibujarSpriteCentrado(inicioX, inicioY - Dibujar.ContarSaltos(titulo) - 2, titulo);

            string subtitulo1 = "En busca del demonio malo malísimo, MUY MUY malo";
            Console.SetCursorPosition(inicioX - (subtitulo1.Length / 2), inicioY);
            Dibujar.EscribirTexto(subtitulo1);
            Dibujar.EscribirTexto("...");

            string subtitulo2 = "Pulse ENTER para comenzar la aventura";
            Console.SetCursorPosition(inicioX - (subtitulo2.Length / 2), inicioY + 2);
            Dibujar.EscribirTexto(subtitulo2);

            Console.CursorVisible = false;

            bool enterPulsado = false;

            while (!enterPulsado)
            {
                if (Console.KeyAvailable)
                {
                    enterPulsado = Console.ReadKey().Key == ConsoleKey.Enter;
                }
            }

            Dibujar.LimpiarPantalla();
            Console.CursorVisible = true;
        }
    }
}

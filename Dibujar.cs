using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Dibujar
    {
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

        public static void DibujarSpriteNormal(int x, int y, string dibujo)
        {
            int anchuraDibujo = GetAnchuraDibujo(dibujo);

            string[] lineas = dibujo.Split('\n');
            for (int i = 0; i < lineas.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

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

        public static void DibujarSpriteCentrado(int x, int y, string dibujo)
        {
            int anchuraDibujo = GetAnchuraDibujo(dibujo);
            int comienzoX = x - (anchuraDibujo / 2);

            string[] lineas = dibujo.Split('\n');
            for (int i = 0; i < lineas.Length; i++)
            {
                Console.SetCursorPosition(comienzoX, y + i);

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

        public static void DibujarRectangulo(int x, int y, char caracter)
        {
            for (int i = 0; i <= Program.ALTURA_RECTANGULO; i++)
            {
                for (int j = 0; j <= Program.ANCHURA_RECTANGULO; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    if (i == 0 || i == Program.ALTURA_RECTANGULO || j == 0 || j == Program.ANCHURA_RECTANGULO)
                    {
                        if (j == Program.ANCHURA_RECTANGULO || i == Program.ALTURA_RECTANGULO)
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

        private static void LimpiarPantallaSimple(int x, int y, int maxAncho, int maxAlto)
        {
            for (int i = x; i < x + maxAncho; i++)
            {
                for (int j = y; j < maxAlto; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }

        public static void LimpiarPantalla(int x, int y, int maxAncho, int maxAlto)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            LimpiarPantallaSimple(x, y, maxAncho, maxAlto);
            Console.ResetColor();
            LimpiarPantallaSimple(x, y, maxAncho, maxAlto);
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

        public static void Inicio(int inicioX, int inicioY)
        {
            int x = inicioX + (Program.ANCHURA_RECTANGULO / 2);
            int y = inicioY + (Program.ALTURA_RECTANGULO / 2);

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Dibujar.DibujarSpriteCentrado(x, y - 3, "¡ ADVERTENCIA !");
            Console.ResetColor();

            Dibujar.DibujarSpriteCentrado(x, y - 1, "Se recomienda poner la pantalla completa (F11) para mejor expriencia");
            Dibujar.DibujarSpriteCentrado(x, y + 1, "Para continuar pulse ENTER...");

            bool pantallaCompleta = false;
            while (!pantallaCompleta)
            {
                if (Console.KeyAvailable)
                {
                    pantallaCompleta = Console.ReadKey().Key == ConsoleKey.Enter;
                }
            }
            Console.CursorVisible = true;

            Dibujar.LimpiarPantalla(inicioX, inicioY, Program.ANCHURA_RECTANGULO - 1, Program.ALTURA_RECTANGULO + inicioY - 1);

            string titulo = "\r\n██████╗░██████╗░░██████╗░" +
                            "\r\n██╔══██╗██╔══██╗██╔════╝░" +
                            "\r\n██████╔╝██████╔╝██║░░██╗░" +
                            "\r\n██╔══██╗██╔═══╝░██║░░╚██╗" +
                            "\r\n██║░░██║██║░░░░░╚██████╔╝" +
                            "\r\n╚═╝░░╚═╝╚═╝░░░░░░╚═════╝░";
            Dibujar.DibujarSpriteCentrado(x, y - Dibujar.ContarSaltos(titulo) - 2, titulo);

            string subtitulo1 = "En busca del demonio malo malísimo, MUY MUY malo";
            Console.SetCursorPosition(x - (subtitulo1.Length / 2), y);
            Dibujar.EscribirTexto(subtitulo1);
            Dibujar.EscribirTexto("...");

            string subtitulo2 = "Pulse ENTER para comenzar la aventura";
            Console.SetCursorPosition(x - (subtitulo2.Length / 2), y + 2);
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

            Dibujar.LimpiarPantalla(inicioX, inicioY, Program.ANCHURA_RECTANGULO - 1, Program.ALTURA_RECTANGULO + inicioY - 1);
            Console.CursorVisible = true;
        }
    }
}

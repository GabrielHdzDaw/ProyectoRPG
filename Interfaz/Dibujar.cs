using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Interfaz
{
    internal class Dibujar
    {
        static int anchuraRectangulo = 176; // Dentro el ancho es de 175
        static int alturaRectangulo = 44; // Dentro el ancho es de 43
        static int x = (Console.WindowWidth - anchuraRectangulo) / 2 + 1;
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
            int comienzoX = posicionX - anchuraDibujo / 2;

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
            Console.CursorVisible = false;

            for (int i = 0; i < alturaRectangulo; i++)
            {
                for (int j = 0; j < anchuraRectangulo; j++)
                {
                    if (i == 0 || i == alturaRectangulo - 1 || j == 0 || j == anchuraRectangulo - 1)
                    {
                        if (x + j < Console.BufferWidth && y + i < Console.BufferHeight)
                        {
                            Console.SetCursorPosition(x + j, y + i);
                            Console.Write(caracter);
                        }
                    }
                }
            }

            Console.CursorVisible = true;
        }

        public static void DibujarRectangulo(int posicionX, int posicionY, int alturaMax, int anchuraMax, char caracter)
        {
            Console.CursorVisible = false;

            for (int i = 0; i < alturaMax; i++)
            {
                for (int j = 0; j < anchuraMax; j++)
                {
                    if (i == 0 || i == alturaMax - 1 || j == 0 || j == anchuraMax - 1)
                    {
                        if (posicionX + j < Console.BufferWidth && posicionY + i < Console.BufferHeight)
                        {
                            Console.SetCursorPosition(posicionX + j, posicionY + i);
                            Console.Write(caracter);
                        }
                    }
                }
            }

            Console.CursorVisible = true;
        }


        public static void LimpiarPantallaSimple()
        {
            Console.CursorVisible = false;
            for (int j = y + 1; j < y + alturaRectangulo - 1; j++)
            {
                for (int i = x + 1; i < x + anchuraRectangulo - 1; i++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
            Console.CursorVisible = true;
        }


        public static void LimpiarPantalla()
        {
            Console.BackgroundColor = ConsoleColor.Black;
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
        public static void DibujarCaracter(int x, int y, char c)
        {
            if (x >= 0 && y >= 0 && x < Console.WindowWidth && y < Console.WindowHeight)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(c);
            }
        }
        public static void Inicio()
        {
            int inicioX = x + anchuraRectangulo / 2;
            int inicioY = y + alturaRectangulo / 2;

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            DibujarSpriteCentrado(inicioX, inicioY - 3, "¡ ADVERTENCIA !");
            Console.ResetColor();

            DibujarSpriteCentrado(inicioX, inicioY - 1, "Se recomienda poner la pantalla completa (F11) para mejor expriencia");
            DibujarSpriteCentrado(inicioX, inicioY + 1, "Para continuar pulse ENTER...");

            bool pantallaCompleta = false;
            while (!pantallaCompleta)
            {
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    pantallaCompleta = Console.ReadKey(true).Key == ConsoleKey.Enter;
                }
            }
            Console.CursorVisible = true;

            LimpiarPantalla(); 

            string titulo = "\r\n██████╗░██████╗░░██████╗░" +
                            "\r\n██╔══██╗██╔══██╗██╔════╝░" +
                            "\r\n██████╔╝██████╔╝██║░░██╗░" +
                            "\r\n██╔══██╗██╔═══╝░██║░░╚██╗" +
                            "\r\n██║░░██║██║░░░░░╚██████╔╝" +
                            "\r\n╚═╝░░╚═╝╚═╝░░░░░░╚═════╝░";
            DibujarSpriteCentrado(inicioX, inicioY - ContarSaltos(titulo) - 2, titulo);

            string subtitulo1 = "En busca del demonio malo malísimo, MUY MUY malo";
            Console.SetCursorPosition(inicioX - subtitulo1.Length / 2, inicioY);
            EscribirTexto(subtitulo1);
            EscribirTexto("...");

            string subtitulo2 = "Pulse ENTER para comenzar la aventura...";
            Console.SetCursorPosition(inicioX - subtitulo2.Length / 2, inicioY + 2);
            EscribirTexto(subtitulo2);

            Console.CursorVisible = false;

            bool enterPulsado = false;

            while (!enterPulsado)
            {
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    enterPulsado = Console.ReadKey(true).Key == ConsoleKey.Enter;
                }
            }

            LimpiarPantalla();
            Console.CursorVisible = true;
        }
        public static void Cinematica(int personaje)
        {
            int inicioX = x + anchuraRectangulo / 2;
            int inicioY = y + alturaRectangulo / 2;
            switch (personaje)
            {
                case 0:
                    DibujarSpriteNormal(Convert.ToInt32(Dibujar.X + Dibujar.AnchuraRectangulo / 2.5), Dibujar.Y + Dibujar.AlturaRectangulo / 6 / 2, Sprites.Mago);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DibujarSpriteCentrado(inicioX, inicioY + 8, "¡ Mago !");
                    Console.ResetColor();
                    DibujarSpriteCentrado(inicioX, inicioY + 10, "\"Maelion, el mago errante, conjura hechizos que hasta los dioses temen recordar\"");
                    break;
                case 1:
                    DibujarSpriteNormal(Convert.ToInt32(Dibujar.X + Dibujar.AnchuraRectangulo / 2.5), Dibujar.Y + 2 + Dibujar.AlturaRectangulo / 6 / 2, Sprites.Caballero);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DibujarSpriteCentrado(inicioX, inicioY + 8, "¡ Caballero !");
                    Console.ResetColor();
                    DibujarSpriteCentrado(inicioX, inicioY + 10, "\"Sir Dareth, sin reino ni escudo, lucha por un honor más pesado que su armadura\"");
                    break;
                case 2:
                    DibujarSpriteNormal(Convert.ToInt32(Dibujar.X + Dibujar.AnchuraRectangulo / 2.25), Dibujar.Y + Dibujar.AlturaRectangulo / 6 / 2 + 1, Sprites.Elfo);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DibujarSpriteCentrado(inicioX, inicioY + 8, "¡ Elfo !");
                    Console.ResetColor();
                    DibujarSpriteCentrado(inicioX, inicioY + 10, "\"Elarien, guardián de los bosques antiguos, dispara con la precisión de siglos vividos en silencio\"");
                    break;
                case 3:
                    DibujarSpriteNormal(Convert.ToInt32(Dibujar.X + Dibujar.AnchuraRectangulo / 2.4), Dibujar.Y + Dibujar.AlturaRectangulo / 6 / 2 + 1, Sprites.Picaro);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DibujarSpriteCentrado(inicioX, inicioY + 8, "¡ Picaro !");
                    Console.ResetColor();
                    DibujarSpriteCentrado(inicioX, inicioY + 10, "\"Kael, sombra entre sombras, roba secretos mejor guardados que el oro\"");
                    break;
            }
            DibujarSpriteCentrado(inicioX, inicioY + 12, "Para jugar pulse ENTER...");
            bool jugarMinijuego = false;
            while (!jugarMinijuego)
            {
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    jugarMinijuego = Console.ReadKey(true).Key == ConsoleKey.Enter;
                }
            }
            Console.CursorVisible = true;

        }
    }
}

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
        public static void DibujarSprite(int x, int y, string dibujo)
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

        public static void DibujarRectangulo(int x, int y, int anchura, int altura, char caracter)
        {
            for (int i = 0; i <= altura; i++)
            {
                for (int j = 0; j <= anchura; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    if (i == 0 || i == altura || j == 0 || j == anchura)
                    {
                        if (j == anchura || i == altura)
                        {
                            Console.Write(caracter + "\n");
                        }
                        else
                        {
                            Console.Write(caracter);
                        }
                    }
                }
            }
        }
    }
}

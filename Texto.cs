using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Texto
    {
        int x;
        int y;
        int tiempoEntrePulsaciones;
        string texto;


        public Texto(int x, int y, int tiempoEntrePulsaciones, string texto)
        {
            this.x = x;
            this.y = y;
            this.tiempoEntrePulsaciones = tiempoEntrePulsaciones;
            this.texto = texto;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public int GetTiempoEntrePulsaciones()
        {
            return tiempoEntrePulsaciones;
        }

        public string GetTexto()
        {
            return texto;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public void SetTiempoEntrePulsaciones(int tiempoEntrePulsaciones)
        {
            this.tiempoEntrePulsaciones = tiempoEntrePulsaciones;
        }

        public void SetTexto(string texto)
        {
            this.texto = texto;
        }

        public void DrawText()
        {
            Console.CursorVisible = false;
            if (x >= 0 && y >= 0 && x < Console.WindowWidth && y < Console.WindowHeight)
            {
                Console.SetCursorPosition(x, y);
                for (int i = 0; i < texto.Length; i++)
                {
                    Console.Write(texto[i]);

                    if (tiempoEntrePulsaciones > 0)
                    {
                        //textClickSound.Play();
                        Thread.Sleep(tiempoEntrePulsaciones);
                    }
                }
            }
            else
            {
                Console.WriteLine("Fuera de los límites");
            }
        }

        public override string ToString()
        {
            return $"Texto: {texto} en ({x}, {y})";
        }
    }
}

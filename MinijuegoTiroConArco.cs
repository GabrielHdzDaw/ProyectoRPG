using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class MinijuegoTiroConArco:Minijuego
    {
        int puntuacion = 0;

        int tiradas = 3;
        int ancho = 80; // puedes ajustar esto o usar Console.WindowWidth
        int alto = 25;
        int top = 0;

        public MinijuegoTiroConArco() { }

        public void Empezar()
        {
            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < tiradas; i++)
            {
                int y = SeleccionarLineaHorizontal();
                int x = SeleccionarLineaVertical(y);
   
                puntuacion += MostrarImpacto(x,y);
            }
            
            Console.CursorVisible = true;
            Console.WriteLine($"Has ganado: {puntuacion} puntos");
            
        }

        static int SeleccionarLineaHorizontal()
        {
            int y = 1;
            int direccion = 1;

            while (!Console.KeyAvailable)
            {
                DibujarDiana();

                for (int x = 1; x < Console.WindowWidth - 1; x++)
                    Dibujar.DibujarCaracter(x, y, '-');

                Thread.Sleep(100);
                y += direccion;

                if (y == Console.WindowHeight - 2 || y == 1)
                    direccion *= -1;
            }

            Console.ReadKey(true); // consumir la tecla
            return y;
        }

        static int SeleccionarLineaVertical(int yFijo)
        {
            int x = 1;
            int direccion = 1;

            while (!Console.KeyAvailable)
            {
                DibujarDiana();

                for (int i = 1; i < Console.WindowWidth - 1; i++)
                    Dibujar.DibujarCaracter(i, yFijo, '-');

                for (int j = 1; j < Console.WindowHeight - 1; j++)
                    Dibujar.DibujarCaracter(x, j, '|');

                Thread.Sleep(1);
                x += direccion;

                if (x == Console.WindowWidth - 2 || x == 1)
                    direccion *= -1;
            }

            Console.ReadKey(true);
            return x;
        }

        static int MostrarImpacto(int x, int y)
        {
            DibujarDiana();

            for (int i = 1; i < Console.WindowWidth - 1; i++)
                Dibujar.DibujarCaracter(i, y, '-');

            for (int j = 1; j < Console.WindowHeight - 1; j++)
                Dibujar.DibujarCaracter(x, j, '|');

            Dibujar.DibujarCaracter(x, y, 'X');

            int puntos = CalcularPuntos(x, y);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine("¡Disparo realizado! Has ganado " + puntos + " puntos.");

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Pulsa cualquier tecla para continuar...");
            Console.ReadKey(true);

            return puntos;
        }


        static void DibujarDiana()
        {
            Console.Clear();

            int anchoConsola = Console.WindowWidth;
            int altoConsola = Console.WindowHeight;

            // Tamaños de los 3 cuadros (grande, medio, pequeño)
            int[] anchos = { 20, 14, 8 };
            int[] altos = { 10, 6, 2 };
            char[] caracteres = { Dibujar.Caracter, Dibujar.Caracter, Dibujar.Caracter };

            for (int i = 0; i < 3; i++)
            {
                int anchura = anchos[i];
                int altura = altos[i];

                int posX = (anchoConsola - anchura) / 2;
                int posY = (altoConsola - altura) / 2;

                Dibujar.DibujarRectangulo(posX, posY, altura, anchura, caracteres[i]);
            }
        }
        static int CalcularPuntos(int x, int y)
        {
            int anchoConsola = Console.WindowWidth;
            int altoConsola = Console.WindowHeight;

            int[] anchos = { 20, 14, 8 };
            int[] altos = { 10, 6, 2 };
            int[] puntos = { 20, 50, 100 }; // grande, medio, centro

            for (int i = 2; i >= 0; i--) // del centro hacia afuera
            {
                int anchura = anchos[i];
                int altura = altos[i];

                int posX = (anchoConsola - anchura) / 2;
                int posY = (altoConsola - altura) / 2;

                if (x >= posX && x <= posX + anchura && y >= posY && y <= posY + altura)
                {
                    return puntos[i];
                }
            }

            return 0; // fuera de la diana
        }




    }
}

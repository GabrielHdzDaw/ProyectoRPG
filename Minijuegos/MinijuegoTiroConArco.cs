using System;
using System.Threading;
using ProyectoRPG.Interfaz;

namespace ProyectoRPG.Minijuegos
{
    internal class MinijuegoTiroConArco : Minijuego
    {
        private int puntuacion = 0;
        private int tiradas = 3;

        public MinijuegoTiroConArco() { }

        public bool Jugar()
        {
            Console.CursorVisible = false;

            for (int i = 0; i < tiradas; i++)
            {
                int y = SeleccionarLineaHorizontal();
                int x = SeleccionarLineaVertical(y);

                puntuacion += MostrarImpacto(x, y);
            }

            Console.Clear();
            Dibujar.DibujarRectanguloPrincipal();
            // Mostrar si se ha ganado o no, omitir de la línea 97 a la 102
            return puntuacion > 150;
        }

        private int SeleccionarLineaHorizontal()
        {
            int y = 1;
            int direccion = 1;

            while (!Console.KeyAvailable)
            {
                DibujarDiana();

                for (int x = 1; x < Console.WindowWidth - 1; x++)
                    Dibujar.DibujarCaracter(x, y, '-');

                Dibujar.DibujarRectanguloPrincipal();
                Thread.Sleep(100);
                y += direccion;

                if (y == Console.WindowHeight - 2 || y == 1)
                    direccion *= -1;
            }

            Console.ReadKey(true);
            return y;
        }

        private int SeleccionarLineaVertical(int yFijo)
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

                Dibujar.DibujarRectanguloPrincipal();
                Thread.Sleep(10);
                x += direccion;

                if (x == Console.WindowWidth - 2 || x == 1)
                    direccion *= -1;
            }

            Console.ReadKey(true);
            return x;
        }

        private int MostrarImpacto(int x, int y)
        {
            DibujarDiana();

            for (int i = 1; i < Console.WindowWidth - 1; i++)
                Dibujar.DibujarCaracter(i, y, '-');

            for (int j = 1; j < Console.WindowHeight - 1; j++)
                Dibujar.DibujarCaracter(x, j, '|');

            Dibujar.DibujarCaracter(x, y, 'X');

            int puntos = CalcularPuntos(x, y);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"¡Disparo realizado! Has ganado {puntos} puntos.");

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Pulsa cualquier tecla para continuar...");
            Console.ReadKey(true);

            return puntos;
        }

        private void DibujarDiana()
        {
            Console.Clear();
            int anchoConsola = Console.WindowWidth;
            int altoConsola = Console.WindowHeight;

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

        private int CalcularPuntos(int x, int y)
        {
            int anchoConsola = Console.WindowWidth;
            int altoConsola = Console.WindowHeight;

            int[] anchos = { 20, 14, 8 };
            int[] altos = { 10, 6, 2 };
            int[] puntos = { 20, 50, 100 };

            for (int i = 2; i >= 0; i--)
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

            return 0;
        }
    }
}

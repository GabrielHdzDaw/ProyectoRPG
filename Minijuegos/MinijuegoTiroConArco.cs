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
                Console.CursorVisible = false;
                int y = SeleccionarLineaHorizontal();
                int x = SeleccionarLineaVertical(y);

                puntuacion += MostrarImpacto(x, y);
            }

            if (tiradas == 3)
            {
                Console.SetCursorPosition(Dibujar.X + 2, Dibujar.AlturaRectangulo - 39);
                Console.WriteLine($"¡Tu puntuación total es de {puntuacion} puntos!".PadRight(60));
                Console.SetCursorPosition(Dibujar.X + 2, Dibujar.AlturaRectangulo - 37);
                
                Console.WriteLine("Pulsa cualquier tecla para continuar...".PadRight(60));
                Console.CursorVisible = false;
                Console.ReadKey(true);
            }

            Console.Clear();
            Dibujar.DibujarRectanguloPrincipal();
            // Mostrar si se ha ganado o no, omitir de la línea 97 a la 102
            return puntuacion > 150;
        }

        private int SeleccionarLineaHorizontal()
        {
            int y = Dibujar.Y + 1;
            int direccion = 1;

            while (!Console.KeyAvailable)
            {
                Console.CursorVisible = false;
                DibujarDiana();

                for (int x = Dibujar.X + 1; x < Dibujar.X + Dibujar.AnchuraRectangulo - 1; x++)
                {
                    Console.CursorVisible = false;
                    Dibujar.DibujarCaracter(x, y, '-');
                }
                Console.CursorVisible = false;
                Dibujar.DibujarRectanguloPrincipal();
                Thread.Sleep(100);
                y += direccion;

                if (y == Dibujar.Y + Dibujar.AlturaRectangulo - 2 || y == Dibujar.Y + 1)
                    direccion *= -1;
            }

            Console.ReadKey(true);
            return y;
        }

        private int SeleccionarLineaVertical(int yFijo)
        {
            int x = Dibujar.X + 1;
            int direccion = 1;

            while (!Console.KeyAvailable)
            {
                Console.CursorVisible = false;
                DibujarDiana();

                for (int i = Dibujar.X + 1; i < Dibujar.X + Dibujar.AnchuraRectangulo - 1; i++)
                {
                    Console.CursorVisible = false;
                    Dibujar.DibujarCaracter(i, yFijo, '-');
                }
                Console.CursorVisible = false;
                for (int j = Dibujar.Y + 1; j < Dibujar.Y + Dibujar.AlturaRectangulo - 1; j++)
                {
                    Console.CursorVisible = false;
                    Dibujar.DibujarCaracter(x, j, '|');
                }
                Console.CursorVisible = false;
                Dibujar.DibujarRectanguloPrincipal();
                Thread.Sleep(10);
                x += direccion;

                if (x == Dibujar.X + Dibujar.AnchuraRectangulo - 2 || x == Dibujar.X + 1)
                    direccion *= -1;
            }

            Console.ReadKey(true);
            return x;
        }

        private int MostrarImpacto(int x, int y)
        {
            DibujarDiana();

            for (int i = Dibujar.X + 1; i < Dibujar.X + Dibujar.AnchuraRectangulo - 1; i++)
                Dibujar.DibujarCaracter(i, y, '-');

            for (int j = Dibujar.Y + 1; j < Dibujar.Y + Dibujar.AlturaRectangulo - 1; j++)
                Dibujar.DibujarCaracter(x, j, '|');

            Dibujar.DibujarCaracter(x, y, 'X');

            int puntos = CalcularPuntos(x, y);

            Dibujar.DibujarRectanguloPrincipal();

            Console.SetCursorPosition(Dibujar.X + 2, Dibujar.AlturaRectangulo - 39);
            Console.WriteLine($"¡Disparo realizado! Has ganado {puntos} puntos.".PadRight(60));

            Console.SetCursorPosition(Dibujar.X + 2, Dibujar.AlturaRectangulo - 37);
            Console.WriteLine("Pulsa cualquier tecla para continuar...".PadRight(60));
            Console.CursorVisible = false;
            Console.ReadKey(true);

            return puntos;
        }

        private void DibujarDiana()
        {
            Console.Clear();

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2;

            int[] anchos = { 20, 14, 8 };
            int[] altos = { 10, 6, 2 };
            char[] caracteres = { Dibujar.Caracter, Dibujar.Caracter, Dibujar.Caracter };

            for (int i = 0; i < 3; i++)
            {
                Console.CursorVisible = false;
                int anchura = anchos[i];
                int altura = altos[i];

                int posX = centroX - anchura / 2;
                int posY = centroY - altura / 2;

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

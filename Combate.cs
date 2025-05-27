using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Combate
    {
        Jugador jugador { get; set; }
        Enemigo enemigo { get; set; }

        public Combate(Jugador jugador, Enemigo enemigo)
        {
            this.jugador = jugador;
            this.enemigo = enemigo;
        }

        public void DibujarInterfazCombate()
        {
            Dibujar.DibujarRectanguloPrincipal();

            int alturaSubRect = 10;
            int yBase = Dibujar.Y + Dibujar.AlturaRectangulo - alturaSubRect;


            int ancho1 = 58;
            int ancho2 = 60;
            int ancho3 = 58;

            int x1 = Dibujar.X;
            int x2 = x1 + ancho1;
            int x3 = x2 + ancho2;

            Dibujar.DibujarRectangulo(x1, yBase, alturaSubRect, ancho1, '▓');
            Dibujar.DibujarRectangulo(x2, yBase, alturaSubRect, ancho2, '▓');
            Dibujar.DibujarRectangulo(x3, yBase, alturaSubRect, ancho3, '▓');

            DibujarSpritesCombate();

            Console.ReadLine();
        }

        public void DibujarSpritesCombate()
        {
            int anchoJugador = jugador.GetSprite().Split('\n').Max(line => line.Length);
            int altoJugador = jugador.GetSprite().Split('\n').Length;

            int anchoEnemigo = enemigo.GetSprite().Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.GetSprite().Split('\n').Length;

            int altoMayor = Math.Max(altoJugador, altoEnemigo);
            int separacion = 20;

            int totalAnchoSprites = anchoJugador + separacion + anchoEnemigo;
            int centroPantallaX = Dibujar.X + (Dibujar.AnchuraRectangulo / 2) - 3;

            int jugadorX = centroPantallaX - (totalAnchoSprites / 2);
            int enemigoX = jugadorX + anchoJugador + separacion;

            int spriteY = Dibujar.Y + (Dibujar.AlturaRectangulo - altoMayor) / 2 - 3;

            Dibujar.DibujarSpriteNormal(jugadorX, spriteY, jugador.GetSprite());
            Dibujar.DibujarSpriteNormal(enemigoX, spriteY, enemigo.GetSprite());
            DibujarBarrasVida();
        }

        public void DibujarBarrasVida()
        {
            int anchoJugador = jugador.GetSprite().Split('\n').Max(line => line.Length);
            int altoJugador = jugador.GetSprite().Split('\n').Length;
            int anchoEnemigo = enemigo.GetSprite().Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.GetSprite().Split('\n').Length;
            int altoMayor = Math.Max(altoJugador, altoEnemigo);
            int separacion = 20;
            int totalAnchoSprites = anchoJugador + separacion + anchoEnemigo;
            int centroPantallaX = Dibujar.X + (Dibujar.AnchuraRectangulo / 2);
            int jugadorX = centroPantallaX - (totalAnchoSprites / 2);
            int enemigoX = jugadorX + anchoJugador + separacion;
            int spriteY = Dibujar.Y + (Dibujar.AlturaRectangulo - altoMayor) / 2 - 3;

           
            int anchoBarraVida = 20;
            int offsetY = -2;

            int barraJugadorX = jugadorX + (anchoJugador / 2) - (anchoBarraVida / 2);
            int barraJugadorY = spriteY + offsetY;
            DibujarNombrePersonaje(barraJugadorX, barraJugadorY - 1, anchoBarraVida, jugador.GetNombre());
            DibujarBarraVida(barraJugadorX, barraJugadorY, anchoBarraVida, jugador.GetVida(), jugador.GetVidaMaxima());
 
            int barraEnemigoX = enemigoX + (anchoEnemigo / 2) - (anchoBarraVida / 2);
            int barraEnemigoY = spriteY + offsetY;
            DibujarNombrePersonaje(barraEnemigoX, barraEnemigoY - 1, anchoBarraVida, enemigo.GetNombre());
            DibujarBarraVida(barraEnemigoX, barraEnemigoY, anchoBarraVida, enemigo.GetVida(), enemigo.GetVidaMaxima());
        }

        private void DibujarNombrePersonaje(int x, int y, int anchoBarra, string nombre)
        {
            // Centrar el nombre sobre la barra de vida
            int centroX = x + (anchoBarra / 2) - 5;
            int nombreX = centroX - (nombre.Length / 2);

            Console.SetCursorPosition(nombreX, y);
            Console.ForegroundColor = ConsoleColor.White; // Color distintivo para los nombres
            Console.Write(nombre);
            Console.ResetColor();
        }

        private void DibujarBarraVida(int x, int y, int ancho, int vidaActual, int vidaMaxima)
        {
            double porcentajeVida = (double)vidaActual / vidaMaxima;
            int vidaEnBarra = (int)(ancho * porcentajeVida);
   
            ConsoleColor colorVida = DeterminarColorVida(porcentajeVida);
            
            Console.SetCursorPosition(x, y);
           
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");

            Console.ForegroundColor = colorVida;
            for (int i = 0; i < vidaEnBarra; i++)
            {
                Console.Write("█");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = vidaEnBarra; i < ancho; i++)
            {
                Console.Write("░");
            }

            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");

            
            Console.SetCursorPosition(x + ancho + 3, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{vidaActual}/{vidaMaxima}");

            
            Console.ResetColor();
        }

        private ConsoleColor DeterminarColorVida(double porcentaje)
        {
            if (porcentaje > 0.6)
                return ConsoleColor.Green;
            else if (porcentaje > 0.3)
                return ConsoleColor.Yellow;
            else if (porcentaje > 0.1)
                return ConsoleColor.Red;
            else
                return ConsoleColor.DarkRed;
        }


        public void IniciarCombate()
        {

        }

        public override string ToString()
        {
            return "Combate entre " + jugador.ToString() + " y " + enemigo.ToString();
        }
    }
}

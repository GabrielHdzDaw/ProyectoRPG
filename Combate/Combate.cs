using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Personajes;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Combate
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


        }
        public void AnimacionAtaqueSprite(bool esJugador)
        {

            int anchoJugador = jugador.GetSprite().Split('\n').Max(line => line.Length);
            int altoJugador = jugador.GetSprite().Split('\n').Length;
            int anchoEnemigo = enemigo.GetSprite().Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.GetSprite().Split('\n').Length;
            int altoMayor = Math.Max(altoJugador, altoEnemigo);
            int separacion = 20;
            int totalAnchoSprites = anchoJugador + separacion + anchoEnemigo;
            int centroPantallaX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 3;
            int jugadorX = centroPantallaX - totalAnchoSprites / 2;
            int enemigoX = jugadorX + anchoJugador + separacion;
            int spriteY = Dibujar.Y + (Dibujar.AlturaRectangulo - altoMayor) / 2 - 3;

            int pasos = 5;
            int desplazamiento = 2;

            for (int i = 0; i < pasos; i++)
            {
                for (int y = 0; y < altoMayor; y++)
                {
                    Console.SetCursorPosition(jugadorX, spriteY + y);
                    Console.Write(new string(' ', totalAnchoSprites));
                }

                int dxJugador = esJugador ? i * desplazamiento : 0;
                int dxEnemigo = esJugador ? 0 : -i * desplazamiento;

                Dibujar.DibujarSpriteNormal(jugadorX + dxJugador, spriteY, jugador.GetSprite());
                Dibujar.DibujarSpriteNormal(enemigoX + dxEnemigo, spriteY, enemigo.GetSprite());
                DibujarBarrasVida();

                Thread.Sleep(10);
            }

            int parpadeos = 3;
            int delayParpadeo = 30;

            for (int i = 0; i < parpadeos; i++)
            {

                for (int y = 0; y < altoMayor; y++)
                {
                    int x = esJugador ? enemigoX : jugadorX;
                    Console.SetCursorPosition(x, spriteY + y);
                    Console.Write(new string(' ', esJugador ? anchoEnemigo : anchoJugador));
                }

                Thread.Sleep(delayParpadeo);


                string spriteGolpeado = esJugador ? enemigo.GetSprite() : jugador.GetSprite();
                int xSprite = esJugador ? enemigoX : jugadorX;
                Dibujar.DibujarSpriteNormal(xSprite, spriteY, spriteGolpeado);

                Thread.Sleep(delayParpadeo);
            }

            for (int i = pasos - 1; i >= 0; i--)
            {
                for (int y = 0; y < altoMayor; y++)
                {
                    Console.SetCursorPosition(jugadorX, spriteY + y);
                    Console.Write(new string(' ', totalAnchoSprites));
                }

                int dxJugador = esJugador ? i * desplazamiento : 0;
                int dxEnemigo = esJugador ? 0 : -i * desplazamiento;

                Dibujar.DibujarSpriteNormal(jugadorX + dxJugador, spriteY, jugador.GetSprite());
                Dibujar.DibujarSpriteNormal(enemigoX + dxEnemigo, spriteY, enemigo.GetSprite());
                DibujarBarrasVida();

                Thread.Sleep(10);
            }
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
            int centroPantallaX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 3;

            int jugadorX = centroPantallaX - totalAnchoSprites / 2;
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
            int centroPantallaX = Dibujar.X + Dibujar.AnchuraRectangulo / 2;
            int jugadorX = centroPantallaX - totalAnchoSprites / 2;
            int enemigoX = jugadorX + anchoJugador + separacion;
            int spriteY = Dibujar.Y + (Dibujar.AlturaRectangulo - altoMayor) / 2 - 3;

            int anchoBarraVida = 20;
            int offsetY = -2;

            int barraJugadorX = jugadorX + anchoJugador / 2 - anchoBarraVida / 2;
            int barraJugadorY = spriteY + offsetY;
            DibujarNombrePersonaje(barraJugadorX, barraJugadorY - 1, anchoBarraVida, jugador.GetNombre());
            DibujarBarraVida(barraJugadorX, barraJugadorY, anchoBarraVida, jugador.GetVida(), jugador.GetVidaMaxima());

            int barraEnemigoX = enemigoX + anchoEnemigo / 2 - anchoBarraVida / 2;
            int barraEnemigoY = spriteY + offsetY;
            DibujarNombrePersonaje(barraEnemigoX, barraEnemigoY - 1, anchoBarraVida, enemigo.GetNombre());
            DibujarBarraVida(barraEnemigoX, barraEnemigoY, anchoBarraVida, enemigo.GetVida(), enemigo.GetVidaMaxima());
        }

        private void DibujarNombrePersonaje(int x, int y, int anchoBarra, string nombre)
        {

            int centroX = x + anchoBarra / 2 - 5;
            int nombreX = centroX - nombre.Length / 2;

            Console.SetCursorPosition(nombreX, y);
            Console.ForegroundColor = ConsoleColor.White;
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

        private void LimpiarAreaMenu(int menuX, int menuY, string[] opciones)
        {
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.SetCursorPosition(menuX, menuY + i);
                Console.Write(new string(' ', 15));
            }
        }

        public int MostrarMenuCombate()
        {
            Console.CursorVisible = false;
            string[] opciones = { "Atacar", "Defender", "Item", "Huir" };
            int opcion = 0;


            int menuX = Dibujar.X + 2;
            int menuY = Dibujar.Y + Dibujar.AlturaRectangulo - opciones.Length - 3;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            while (tecla.Key != ConsoleKey.Enter)
            {
                LimpiarAreaMenu(menuX, menuY, opciones);

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.SetCursorPosition(menuX, menuY + i);
                    Console.Write($"{opciones[i]}");

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

                Thread.Sleep(50);
            }
            return opcion;
        }

        public Ataque MenuAtaque(int x, int y, int maxAnchura, int maxAltura)
        {
            Console.CursorVisible = false;
            string[] opciones = jugador.Ataques.Select(a => a.ToString()).ToArray();
            int opcion = 0;

            int menuX = Dibujar.X + 12;
            int menuY = Dibujar.Y + Dibujar.AlturaRectangulo - opciones.Length - 3;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            while (tecla.Key != ConsoleKey.Enter)
            {
                LimpiarAreaMenu(menuX, menuY, opciones);

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.SetCursorPosition(menuX, menuY + i);
                    Console.Write($"{opciones[i]}");

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

                Thread.Sleep(50);
            }
            Ataque ataqueSeleccionado = jugador.GetAtaques().ToArray()[opcion];
            return ataqueSeleccionado;
        }

        public Item MenuItem(int x, int y, int maxAnchura, int maxAltura)
        {
            Console.CursorVisible = false;
            List<Pocion> pociones = jugador.GetInventario().Pociones;

            string[] opciones = pociones.Select(i => i.ToString()).ToArray();
            int opcion = 0;
            int menuX = Dibujar.X + 12;
            int menuY = Dibujar.Y + Dibujar.AlturaRectangulo - opciones.Length - 3;
            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            while (tecla.Key != ConsoleKey.Enter)
            {
                LimpiarAreaMenu(menuX, menuY, opciones);
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(menuX, menuY + i);
                    Console.Write($"{opciones[i]}");
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
                Thread.Sleep(50);
            }
            Pocion pocionSeleccionada = pociones[opcion];
            return pocionSeleccionada;
        }

        public bool EmpezarCombate()
        {
            bool combateActivo = true;
            bool victoria = false;

            while (combateActivo && !jugador.EstaMuerto() && !enemigo.EstaMuerto())
            {

                Console.Clear();
                DibujarInterfazCombate();
                MostrarMensaje($"¡Un {enemigo.GetNombre()} te ataca!", false);


                int opcionSeleccionada = MostrarMenuCombate();

                switch (opcionSeleccionada)
                {
                    case 0:
                        EjecutarAtaque();
                        break;
                    case 1:
                        EjecutarDefensa();
                        break;
                    case 2:
                        EjecutarItem();
                        DibujarBarrasVida();
                        break;
                    case 3:
                        if (IntentarHuir())
                        {
                            combateActivo = false;
                            MostrarMensaje("¡Has huido del combate!", true);
                        }
                        else
                        {
                            MostrarMensaje("No puedes huir...", true);
                        }
                        break;
                }

                if (combateActivo && !enemigo.EstaMuerto())
                {
                    EjecutarTurnoEnemigo();
                }

                if (jugador.EstaMuerto())
                {
                    MostrarMensaje("¡Has sido derrotado!", true);
                    combateActivo = false;
                    victoria = false;
                }
                else if (enemigo.EstaMuerto())
                {
                    MostrarMensaje("¡Has ganado el combate!", true);
                    combateActivo = false;
                    victoria = true;
                }

            }
            Dibujar.LimpiarPantalla();
            Console.Clear();
            return victoria;
        }

        private void EjecutarAtaque()
        {
            Ataque ataqueSeleccionado = MenuAtaque(Dibujar.X + 8, Dibujar.Y + Dibujar.AlturaRectangulo - 7, 20, 4);
            Random random = new Random();

            int tirada = random.Next(1, 101);
            if (tirada > ataqueSeleccionado.probabilidad)
            {
                MostrarMensaje($"{jugador.GetNombre()} intenta usar {ataqueSeleccionado.nombre}, ¡pero falla!", true);
                return;
            }

            int dañoBase = jugador.GetAtaque();

            Arma? arma = jugador.GetArma();
            int dañoArma = 0;
            int dañoCriticoArma = 0;
            int probCritico = 0;

            if (arma != null)
            {
                dañoArma = arma.dano;
                dañoCriticoArma = arma.danoCritico;
                probCritico = arma.probabilidadCritico;
            }

            bool esCritico = random.Next(1, 101) <= probCritico;

            int dañoTotal = esCritico
                ? dañoBase + dañoCriticoArma + ataqueSeleccionado.dano
                : dañoBase + dañoArma + ataqueSeleccionado.dano;

            int defensa = enemigo.GetDefensa();
            dañoTotal = Math.Max(0, dañoTotal - defensa);

            enemigo.RecibirDaño(dañoTotal);

            string mensaje = esCritico
                ? $"{jugador.GetNombre()} ataca con {ataqueSeleccionado.nombre} y causa {dañoTotal} de impacto crítico!"
                : $"{jugador.GetNombre()} ataca con {ataqueSeleccionado.nombre} y causa {dañoTotal} de daño!";

            MostrarMensaje(mensaje, true);
            AnimacionAtaqueSprite(true);
        }

        private void EjecutarDefensa()
        {
            MostrarMensaje($"{jugador.GetNombre()} se defiende. Recibirá menos daño este turno.", true);
            jugador.SetDefensa(jugador.GetDefensa() + 2);

        }

        private void EjecutarItem()
        {
            Pocion pocion = (Pocion)MenuItem(Dibujar.X + 8, Dibujar.Y + Dibujar.AlturaRectangulo - 7, 20, 4);
            if (jugador.GetInventario().ContieneObjeto(pocion))
            {
                jugador.UsarPocion(pocion);
                jugador.GetInventario().EliminarObjeto(pocion);
                MostrarMensaje($"{jugador.GetNombre()} usa {pocion.GetNombre()} y recupera {pocion.curacion} de vida.", true);
            }
            else
            {
                MostrarMensaje("No tienes esa poción en tu inventario.", true);
            }
        }

        private bool IntentarHuir()
        {
            Random random = new Random();
            int probabilidadHuir = jugador.GetVelocidad() * 100 / (jugador.GetVelocidad() + enemigo.GetVelocidad());
            return random.Next(0, 100) < probabilidadHuir;
        }

        private void EjecutarTurnoEnemigo()
        {
            int dano = CalcularDaño(enemigo, jugador);
            jugador.RecibirDaño(dano);
            MostrarMensaje($"{enemigo.GetNombre()} ataca a {jugador.GetNombre()} causando {dano} de daño!", true);
            AnimacionAtaqueSprite(false);
        }

        private int CalcularDaño(Personaje atacante, Personaje defensor)
        {
            Random random = new Random();
            int danoBase = atacante.GetAtaque() - defensor.GetDefensa();
            int variacion = random.Next(-2, 3);
            return Math.Max(1, danoBase + variacion);
        }

        private void MostrarMensaje(string mensaje, bool espera)
        {
            BorrarMensaje();
            int mensajeX = Dibujar.AnchuraRectangulo / 2 + 5;
            int mensajeY = Dibujar.AlturaRectangulo / 4 * 4 + 5;

            Console.SetCursorPosition(mensajeX, mensajeY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(mensaje);
            Console.ResetColor();
            if (espera)
            {
                Thread.Sleep(1000);
            }

        }

        private void BorrarMensaje()
        {
            int mensajeX = Dibujar.AnchuraRectangulo / 2 + 4;
            int mensajeY = Dibujar.AlturaRectangulo / 4 * 4 + 5;
            Console.SetCursorPosition(mensajeX, mensajeY);
            Console.Write(new string(' ', 58));
        }

        public override string ToString()
        {
            return "Combate entre " + jugador.ToString() + " y " + enemigo.ToString();
        }
    }
}

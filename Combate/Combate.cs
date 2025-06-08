using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Personajes;
using ProyectoRPG.Inventario;
using ProyectoRPG.Recursos;
using ProyectoRPG.Sistema;


namespace ProyectoRPG.Combate
{
    public class Combate
    {
        Partida partida { get; set; }
        Enemigo enemigo { get; set; }

        public static Enemigo demonio { get; set; } = new Enemigo("Demonio Malo Malísimo", Sprites.Demonio, 500, 25, 30, 20);

        public Combate(Partida partida, Enemigo enemigo)
        {
            this.partida = partida;
            this.enemigo = enemigo;
        }

        

        public void DibujarInterfazCombate()
        {
           
            Dibujar.DibujarRectanguloPrincipal();
            int alturaSubRect = 10;
            int yBase = Dibujar.Y + Dibujar.AlturaRectangulo - alturaSubRect;


            int ancho1 = 58;
            int ancho2 = 118;
            

            int x1 = Dibujar.X;
            int x2 = x1 + ancho1;

            Dibujar.DibujarRectangulo(x1, yBase, alturaSubRect, ancho1, '▓');
            Dibujar.DibujarRectangulo(x2, yBase, alturaSubRect, ancho2, '▓');

            DibujarSpritesCombate();
        }
        public void AnimacionAtaqueSprite(bool esJugador)
        {

            int anchoJugador = partida.jugador.Sprite.Split('\n').Max(line => line.Length);
            int altoJugador = partida.jugador.Sprite.Split('\n').Length;
            int anchoEnemigo = enemigo.Sprite.Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.Sprite.Split('\n').Length;
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

                Dibujar.DibujarSpriteNormal(jugadorX + dxJugador, spriteY, partida.jugador.Sprite);
                Dibujar.DibujarSpriteNormal(enemigoX + dxEnemigo, spriteY, enemigo.Sprite);
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


                string spriteGolpeado = esJugador ? enemigo.Sprite : partida.jugador.Sprite;
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

                Dibujar.DibujarSpriteNormal(jugadorX + dxJugador, spriteY, partida.jugador.Sprite);
                Dibujar.DibujarSpriteNormal(enemigoX + dxEnemigo, spriteY, enemigo.Sprite);
                DibujarBarrasVida();

                Thread.Sleep(10);
            }
        }

        public void DibujarSpritesCombate()
        {
            int anchoJugador = partida.jugador.Sprite.Split('\n').Max(line => line.Length);
            int altoJugador = partida.jugador.Sprite.Split('\n').Length;

            int anchoEnemigo = enemigo.Sprite.Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.Sprite.Split('\n').Length;

            int altoMayor = Math.Max(altoJugador, altoEnemigo);
            int separacion = 20;

            int totalAnchoSprites = anchoJugador + separacion + anchoEnemigo;
            int centroPantallaX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 3;

            int jugadorX = centroPantallaX - totalAnchoSprites / 2;
            int enemigoX = jugadorX + anchoJugador + separacion;

            int spriteY = Dibujar.Y + (Dibujar.AlturaRectangulo - altoMayor) / 2 - 3;

            Dibujar.DibujarSpriteNormal(jugadorX, spriteY, partida.jugador.Sprite);
            Dibujar.DibujarSpriteNormal(enemigoX, spriteY, enemigo.Sprite);
            DibujarBarrasVida();
        }

        public void DibujarBarrasVida()
        {
            int anchoJugador = partida.jugador.Sprite.Split('\n').Max(line => line.Length);
            int altoJugador = partida.jugador.Sprite.Split('\n').Length;
            int anchoEnemigo = enemigo.Sprite.Split('\n').Max(line => line.Length);
            int altoEnemigo = enemigo.Sprite.Split('\n').Length;
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
            DibujarNombrePersonaje(barraJugadorX, barraJugadorY - 1, anchoBarraVida, partida.jugador.Nombre);
            DibujarBarraVida(barraJugadorX, barraJugadorY, anchoBarraVida, partida.jugador.Vida, partida.jugador.VidaMaxima);

            int barraEnemigoX = enemigoX + anchoEnemigo / 2 - anchoBarraVida / 2;
            int barraEnemigoY = spriteY + offsetY;
            DibujarNombrePersonaje(barraEnemigoX, barraEnemigoY - 1, anchoBarraVida, enemigo.Nombre);
            DibujarBarraVida(barraEnemigoX, barraEnemigoY, anchoBarraVida, enemigo.Vida, enemigo.VidaMaxima);
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
            string[] opciones = partida.jugador.Ataques.Select(a => a.ToString()).ToArray();
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
            Ataque ataqueSeleccionado = partida.jugador.GetAtaques().ToArray()[opcion];
            return ataqueSeleccionado;
        }

        public Item MenuItem(int x, int y, int maxAnchura, int maxAltura)
        {
            Console.CursorVisible = false;
            List<Pocion> pociones = partida.jugador.GetInventario().Pociones;

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

        public void OtorgarPocion()
        {
            Pocion pocion = new Pocion(50);
            partida.jugador.GetInventario().AgregarObjeto(pocion);
        }

        public bool EmpezarCombate()
        {
            bool esJefeFinal = enemigo.Nombre == "Demonio Malo Malísimo";
            bool combateActivo = true;
            bool victoria = false;

            while (combateActivo && !partida.jugador.EstaMuerto() && !enemigo.EstaMuerto())
            {

                Console.Clear();
                DibujarInterfazCombate();
                MostrarMensaje($"¡Un {enemigo.Nombre} te ataca!", false);


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
                        if (!esJefeFinal)
                        {
                            if (IntentarHuir())
                            {
                                combateActivo = false;
                                MostrarMensaje("¡Has huido del combate!", true);
                            }
                            else
                            {
                                MostrarMensaje("No puedes huir...", true);
                            }
                            continue;
                        }
                        else
                        {
                            MostrarMensaje("¿A dónde crees que vas?", true);
                            continue;
                        }

                        break;
                }

                if (combateActivo && !enemigo.EstaMuerto())
                {
                    EjecutarTurnoEnemigo();
                }

                if (partida.jugador.EstaMuerto())
                {
                    MostrarMensaje("¡Has sido derrotado!", true);
                    combateActivo = false;
                    victoria = false;
                }
                else if (enemigo.EstaMuerto())
                {
                    MostrarMensaje("¡Has ganado el combate!", true);
                    Random randomPocion = new Random();
                    Random randomPico = new Random();
                    if (randomPico.Next(0, 100) < 25)
                    {
                        partida.jugador.GetInventario().AgregarObjetoClave(new ObjetoClave("Pico de escalada", "Con este pico llegarás a lo más alto de la montaña del Demonio Malo Malísimo", 0));
                        MostrarMensaje("¡Has encontrado un pico! ¡Ya puedes subir a la montaña!", true);
                    }
                    if (randomPocion.Next(0, 100) < 50)
                    {
                        OtorgarPocion();
                        MostrarMensaje("¡Has encontrado una poción!", true);
                        MostrarMensaje($"Puntuación: {partida.puntuacion} + 10 = {partida.puntuacion + 10}", true);
                        partida.puntuacion += 10;
                    }

                    combateActivo = false;
                    victoria = true;
                }

            }
            Dibujar.LimpiarPantalla();
            Console.CursorVisible = false;
            return victoria;
        }

        private void EjecutarAtaque()
        {
            Ataque ataqueSeleccionado = MenuAtaque(Dibujar.X + 8, Dibujar.Y + Dibujar.AlturaRectangulo - 7, 20, 4);
            Random random = new Random();

            int tirada = random.Next(1, 101);
            if (tirada > ataqueSeleccionado.probabilidad)
            {
                MostrarMensaje($"{partida.jugador.Nombre} intenta usar {ataqueSeleccionado.nombre}, ¡pero falla!", true);
                return;
            }

            int dañoBase = partida.jugador.Ataque;

            Arma? arma = partida.jugador.GetArma();
            int dañoArma = 0;
            int dañoCriticoArma = 0;
            int probCritico = 0;

            if (arma != null)
            {
                dañoArma = arma.Dano;
                dañoCriticoArma = arma.DanoCritico;
                probCritico = arma.ProbabilidadCritico;
            }

            bool esCritico = random.Next(1, 101) <= probCritico;

            int dañoTotal = esCritico
                ? dañoBase + dañoCriticoArma + ataqueSeleccionado.dano
                : dañoBase + dañoArma + ataqueSeleccionado.dano;

            int defensa = enemigo.Defensa;
            dañoTotal = Math.Max(0, dañoTotal - defensa);

            enemigo.RecibirDaño(dañoTotal);

            string mensaje = esCritico
                ? $"{partida.jugador.Nombre} ataca con {ataqueSeleccionado.nombre} y causa {dañoTotal} de impacto crítico!"
                : $"{partida.jugador.Nombre} ataca con {ataqueSeleccionado.nombre} y causa {dañoTotal} de daño!";

            MostrarMensaje(mensaje, true);
            AnimacionAtaqueSprite(true);
        }

        private void EjecutarDefensa()
        {
            MostrarMensaje($"{partida.jugador.Nombre} se defiende. Recibirá menos daño este turno.", true);
            partida.jugador.Defensa = partida.jugador.Defensa + 2;
        }

        private void EjecutarItem()
        {
            Pocion pocion = (Pocion)MenuItem(Dibujar.X + 8, Dibujar.Y + Dibujar.AlturaRectangulo - 7, 20, 4);
            if (partida.jugador.GetInventario().ContieneObjeto(pocion))
            {
                partida.jugador.UsarPocion(pocion);
                partida.jugador.GetInventario().EliminarObjeto(pocion);
                MostrarMensaje($"{partida.jugador.Nombre} usa {pocion.Nombre} y recupera {pocion.curacion} de vida.", true);
            }
            else
            {
                MostrarMensaje("No tienes esa poción en tu inventario.", true);
            }
        }

        private bool IntentarHuir()
        {
            Random random = new Random();
            int probabilidadHuir = partida.jugador.Velocidad * 100 / (partida.jugador.Velocidad + enemigo.Velocidad);
            return random.Next(0, 100) < probabilidadHuir;
        }

        private void EjecutarTurnoEnemigo()
        {
            int dano = CalcularDaño(enemigo, partida.jugador);
            partida.jugador.RecibirDaño(dano);
            MostrarMensaje($"{enemigo.Nombre} ataca a {partida.jugador.Nombre} causando {dano} de daño!", true);
            AnimacionAtaqueSprite(false);
        }

        private int CalcularDaño(Personaje atacante, Personaje defensor)
        {
            Random random = new Random();
            int danoBase = atacante.Ataque - defensor.Defensa;
            int variacion = random.Next(-2, 3);
            return Math.Max(1, danoBase + variacion);
        }

        private void MostrarMensaje(string mensaje, bool espera)
        {
            BorrarMensaje();
            int mensajeX = Dibujar.AnchuraRectangulo / 2 + 25;
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
            int mensajeX = Dibujar.AnchuraRectangulo / 2 + 25;
            int mensajeY = Dibujar.AlturaRectangulo / 4 * 4 + 5;
            Console.SetCursorPosition(mensajeX, mensajeY);
            Console.Write(new string(' ', 58));
        }

        public override string ToString()
        {
            return "Combate entre " + partida.jugador.ToString() + " y " + enemigo.ToString();
        }
    }
}

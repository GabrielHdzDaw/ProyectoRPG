using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Minijuegos;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Sistema
{
    class Partida
    {
        Jugador jugador { get; set; }
        int puntuacion { get; set; }
        bool terminada { get; set; }

        public Partida()
        { }

        public Partida(Jugador jugador)
        {
            this.jugador = jugador;
            puntuacion = 0;
            terminada = false;
        }

        private static string NombreElegido()
        {
            string nombreUsuario = "";
            bool escritoMal = false;

            do
            {
                if (escritoMal)
                {
                    Dibujar.LimpiarPantallaSimple();
                }
                else
                {
                    Dibujar.LimpiarPantalla();
                }

                nombreUsuario = PedirNombreUsuario(escritoMal);
                Dibujar.DibujarRectanguloPrincipal();
                escritoMal = NombreUsuarioValido(nombreUsuario);
            } while (escritoMal);

            Dibujar.LimpiarPantalla();

            return nombreUsuario;
        }

        private static string PedirNombreUsuario(bool escritoMal)
        {
            if (escritoMal)
            {
                Console.SetCursorPosition(Dibujar.X + 2, Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: El nombre debe estar entre 1 y 30 caracteres y no debe usar caracteres especiales o estar ya registrado.");
                Console.ResetColor();
            }

            Console.SetCursorPosition(Dibujar.X + 2, Dibujar.Y + Dibujar.AlturaRectangulo / 2);
            Console.Write("Nombre del jugador (30 caracteres): ");
            return Console.ReadLine();
        }

        private static bool NombreUsuarioValido(string nombre)
        {
            string cadenaCaracteresNoValidos = "ºª\\!|\"@·#$~%€&¬/()='?¡¿`^[+*]´¨{ç},;.:-_<>";

            List<FileInfo> ficheros = new List<FileInfo>(new DirectoryInfo("./jugadores").GetFiles());
            List<string> nombresArchivos = ficheros.Select(f => f.Name).ToList();

            bool caracterNoValido = false;
            for (int i = 0; i < cadenaCaracteresNoValidos.Length && !caracterNoValido; i++)
            {
                caracterNoValido = nombre.Contains(cadenaCaracteresNoValidos[i]);
            }

            if (nombre.Length > 30)
            {
                Console.Clear();
                Dibujar.DibujarRectanguloPrincipal();
            }

            return nombre.Length == 0 || nombre.Length > 30 || caracterNoValido || nombresArchivos.Contains($"{nombre}.json");
        }

        private static int ClaseElegida()
        {
            string[] opciones = ["Mago", "Caballero", "Elfo", "Picaro"];
            string[] textosPersonajes = ["Un personaje que vive en una constante batalla mental",
                                         "No conocerás a ser más noble, pero eso no quita que tenga sus vicios",
                                         "El guardian de los bosques y el un gran guerrero a la distancia",
                                         "Un humano con una afición por conseguir de formas inapropiadas cosas que no son suyas"];
            int indice = 0;

            int xSprite = (int)(Dibujar.AnchuraRectangulo / 1.40) - 5;
            int ySprite = Dibujar.Y + Dibujar.AlturaRectangulo / 4 - 5;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            bool personajeElegido = false;
            while (!personajeElegido)
            {
                Console.CursorVisible = false;

                int espaciadoVertical = 1;
                Dibujar.DibujarRectanguloPrincipal();
                Dibujar.DibujarRectangulo(Dibujar.X, Dibujar.Y, Dibujar.AlturaRectangulo, Dibujar.AnchuraRectangulo / 4, Dibujar.Caracter);

                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.CursorVisible = false;
                    if (indice == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Dibujar.DibujarSpriteCentrado(Dibujar.X + Dibujar.AnchuraRectangulo / 4 / 2, Dibujar.Y + Dibujar.AlturaRectangulo / 4 - espaciadoVertical, opciones[i]);
                    espaciadoVertical -= 8;

                    if (indice == i)
                    {
                        Console.ResetColor();
                    }

                    switch (indice)
                    {
                        case 0:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2, Sprites.Mago);
                            break;
                        case 1:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2, Dibujar.Y + 2 + Dibujar.AlturaRectangulo / 4 / 2, Sprites.Caballero);
                            break;
                        case 2:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 + 6, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2 + 1, Sprites.Elfo);
                            break;
                        case 3:
                            Dibujar.DibujarSpriteNormal(Dibujar.X + Dibujar.AnchuraRectangulo / 2 + 2, Dibujar.Y + Dibujar.AlturaRectangulo / 4 / 2 + 1, Sprites.Picaro);
                            break;
                    }

                    Dibujar.DibujarSpriteCentrado(xSprite + (Dibujar.X - 14), Dibujar.AlturaRectangulo + Dibujar.Y - 10, textosPersonajes[indice]);
                }

                if (Console.KeyAvailable)
                {
                    tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (indice - 1 >= 0)
                            {
                                indice--;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (indice + 1 < opciones.Length)
                            {
                                indice++;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.Enter:
                            personajeElegido = true;
                            break;
                    }
                }
            }
            
            Console.CursorVisible = true;
            return indice;
        }

        public static Partida NuevaPartida()
        {
            string nombreUsuario = NombreElegido();
            int personaje = ClaseElegida();
            Dibujar.LimpiarPantalla();
            Dibujar.Cinematica(personaje);

            Dibujar.LimpiarPantallaSimple();

            bool resultado = false;
            Jugador jugador = null;
            switch (personaje)
            {
                case 0:
                    jugador = new Mago(nombreUsuario);
                    MinijuegoPeleaMagos minijuego1 = new MinijuegoPeleaMagos();
                    resultado = minijuego1.Jugar();
                    break;
                case 1:
                    jugador = new Caballero(nombreUsuario);
                    MinijuegoDados minijuego2 = new MinijuegoDados();
                    resultado = minijuego2.Jugar();
                    break;
                case 2:
                    jugador = new Elfo(nombreUsuario);
                    MinijuegoTiroConArco minijuego3 = new MinijuegoTiroConArco();
                    resultado = minijuego3.Jugar();
                    break;
                case 3: 
                    jugador = new Picaro(nombreUsuario);
                    MinijuegoAhorcado minijuego4 = new MinijuegoAhorcado();
                    resultado = minijuego4.Jugar();
                    break;
            }

            Dibujar.LimpiarPantallaSimple();

            Partida partida = new Partida(jugador);
            if(resultado)
            {
                partida.puntuacion = 50;
            }

            partida.GuardarPartida();

            return partida;
        }

        public void Continuar(Partida partida)
        {
            char[,] mapa = AbrirMapa();
            Console.CursorVisible = false;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            while(tecla.Key != ConsoleKey.Escape)
            {
                Dibujar.DibujarMapa(mapa, partida.jugador.x, partida.jugador.y);

                if(Console.KeyAvailable == true)
                {
                    tecla = Console.ReadKey(true);
                    switch(tecla.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            char cUp = mapa[partida.jugador.x, partida.jugador.y - 1];
                            if(cUp != 'A')
                                partida.jugador.y -= 1;
                            break;
                        case ConsoleKey.RightArrow:
                            char cDown = mapa[partida.jugador.x, partida.jugador.y + 1];
                            if (cDown != 'A')
                                partida.jugador.y += 1;
                            break;
                        case ConsoleKey.UpArrow:
                            char cLeft = mapa[partida.jugador.x - 1, partida.jugador.y];
                                if (cLeft != 'A')
                                    partida.jugador.x -= 1;
                            break;
                        case ConsoleKey.DownArrow:
                            char cRight = mapa[partida.jugador.x + 1, partida.jugador.y];
                            if (cRight != 'A')
                                partida.jugador.x += 1;
                            break;
                    }

                    if(mapa[partida.jugador.x, partida.jugador.y] == 'C')
                    {
                        partida.GuardarPartida();
                    }
                }
            }

            Dibujar.LimpiarPantallaSimple();
        }

        private static char[,] AbrirMapa()
        {
            try
            {
                string[] lineas = File.ReadAllLines("./../../../Recursos/mapa.txt");

                int filas = lineas.Length;
                int columnas = lineas[0].Length;

                char[,] matriz = new char[filas, columnas];

                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        matriz[i, j] = lineas[i][j];
                    }
                }

                return matriz;
            }
            catch (IOException ex)
            {
                return new char[0, 0];
            }
        }

        public string NombreArchivo()
        {
            return $"{jugador.GetNombre()}.json";
        }

        public void GuardarPartida()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("./../../../Recursos/jugadores/" + NombreArchivo(), json);
        }

        // Añadir prefijo ruta si fuera necesario (./../../../Recursos/)
        static Partida CargarPartida(string archivo)
        {
            if (!File.Exists(archivo))
                return null;

            string json = File.ReadAllText(archivo);
            Partida partida = JsonSerializer.Deserialize<Partida>(json);

            return partida != null ? partida : new Partida();
        }
    }
}

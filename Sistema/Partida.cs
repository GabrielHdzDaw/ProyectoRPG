using ProyectoRPG.Combate;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Minijuegos;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProyectoRPG.Sistema
{
    public class Partida : IComparable<Partida>
    {
        public Jugador jugador { get; set; }
        public int puntuacion { get; set; }
        public bool terminada { get; set; }

        private static Random random = new Random();
        private static int pasosDesdeUltimoCombate = 0;

        public Partida()
        { }

        public Partida(Jugador jugador)
        {
            this.jugador = jugador;
            puntuacion = 0;
            terminada = false;
        }

        public int CompareTo(Partida other)
        {
            return this.puntuacion.CompareTo(other.puntuacion);
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
                Console.SetCursorPosition(Dibujar.X + 10, Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: El nombre debe estar entre 1 y 30 caracteres y no debe usar caracteres especiales o estar ya registrado.");
                Console.ResetColor();
            }

            Console.SetCursorPosition(Dibujar.X + 10, Dibujar.Y + Dibujar.AlturaRectangulo / 2);
            Console.Write("Nombre del jugador (30 caracteres): ");
            return Console.ReadLine();
        }

        private static bool NombreUsuarioValido(string nombre)
        {
            string cadenaCaracteresNoValidos = "ºª\\!|\"@·#$~%€&¬/()='?¡¿`^[+*]´¨{ç},;.:-_<>";

            List<FileInfo> ficheros = new List<FileInfo>(new DirectoryInfo("./../../../Recursos/jugadores").GetFiles());
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

        public static Partida RenaudarPartida()
        {
            Dibujar.LimpiarPantalla();
            Console.CursorVisible = false;

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;

            string rutaCarpeta = "./../../../Recursos/jugadores";
            string[] archivosJson = Directory.GetFiles(rutaCarpeta, "*.json");


            string[] opciones = new string[archivosJson.Length];

            for (int i = 0; i < archivosJson.Length; i++)
            {
                opciones[i] = Path.GetFileNameWithoutExtension(archivosJson[i]);
            }

            int opcion = 0;

            Dibujar.DibujarSpriteCentrado(centroX - 55, centroY - 15, "Selecciona la partida a continuar: ");

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            string archivo = "";

            // Repetir este bucle hasta que se pulse enter
            while (tecla.Key != ConsoleKey.Enter)
            {
                int espaciadoVertical = 10;
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, $"{i + 1}. " + opciones[i]);
                    espaciadoVertical -= 1;
                    Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, "");
                    espaciadoVertical -= 1;

                    if (opcion == i)
                    {
                        Console.ResetColor();
                    }

                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        archivo = opciones[i] + ".json";
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
            }

            Console.CursorVisible = true;


            Partida continuarPartida = CargarPartida(archivo);

            return continuarPartida;
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
            if (resultado)
            {
                partida.puntuacion = 50;
            }

            partida.GuardarPartida();

            return partida;
        }

        public static bool ProbabilidadCombate()
        {
            pasosDesdeUltimoCombate++;

            double probabilidad = 0.03 + (pasosDesdeUltimoCombate * 0.01);

            if (random.NextDouble() < probabilidad)
            {
                pasosDesdeUltimoCombate = 0;
                return true;
            }
            return false;
        }

        public void Continuar(Partida partida)
        {
            char[,] mapa = AbrirMapa();
            Console.CursorVisible = false;
            ConsoleKeyInfo tecla = new ConsoleKeyInfo();
            while (tecla.Key != ConsoleKey.Escape)
            {
                Dibujar.DibujarMapa(mapa, partida.jugador.x, partida.jugador.y);
                if (Console.KeyAvailable == true)
                {
                    tecla = Console.ReadKey(true);
                    bool seMovio = false;

                    switch (tecla.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            char cUp = mapa[partida.jugador.x, partida.jugador.y - 1];
                            if (cUp != 'A')
                            {
                                partida.jugador.y -= 1;
                                seMovio = true;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            char cDown = mapa[partida.jugador.x, partida.jugador.y + 1];
                            if (cDown != 'A')
                            {
                                partida.jugador.y += 1;
                                seMovio = true;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            char cLeft = mapa[partida.jugador.x - 1, partida.jugador.y];
                            if (cLeft != 'A')
                            {
                                partida.jugador.x -= 1;
                                seMovio = true;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            char cRight = mapa[partida.jugador.x + 1, partida.jugador.y];
                            if (cRight != 'A')
                            {
                                partida.jugador.x += 1;
                                seMovio = true;
                            }
                            break;
                    }

                   
                    if (seMovio && ProbabilidadCombate())
                    {
                        CombateAleatorio combate = new CombateAleatorio(partida);
                        combate.IniciarCombate();
                    }

                    if (mapa[partida.jugador.x, partida.jugador.y] == 'C')
                    {
                        partida.GuardarPartida();
                    }
                }
            }
            Dibujar.LimpiarPantallaSimple();
            Console.CursorVisible = false;
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
            return $"{jugador.Nombre}.json";
        }

        public void GuardarPartida()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyProperties = false // Asegura que los getters se incluyan
            };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText("./../../../Recursos/jugadores/" + NombreArchivo(), json);
        }

        static Partida CargarPartida(string archivo)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyProperties = false // Asegura que los getters se incluyan
            };
            string json = File.ReadAllText("./../../../Recursos/jugadores/" + archivo);
            return JsonSerializer.Deserialize<Partida>(json, options);
        }
    }
}

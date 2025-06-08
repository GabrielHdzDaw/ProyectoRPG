using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using ProyectoRPG.Combate;
using ProyectoRPG.Interfaz;
using ProyectoRPG.Minijuegos;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Sistema
{
    public class Partida : IComparable<Partida>
    {
        public Jugador jugador { get; set; }
        public int puntuacion { get; set; }
        public bool terminada { get; set; }
        public DateTime creacion { get; set; }

        private static Random random = new Random();
        private static int pasosDesdeUltimoCombate = 0;

        public Partida()
        { }

        public Partida(Jugador jugador)
        {
            this.jugador = jugador;
            puntuacion = 0;
            terminada = false;
            creacion = DateTime.Now;
        }

        public int CompareTo(Partida other)
        {
            return other.puntuacion.CompareTo(this.puntuacion);
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

        public static Partida? RenaudarPartida()
        {
            Dibujar.LimpiarPantalla();
            List<Partida> partidas = CargarPartidas();

            List<Partida> partidasNoAcabadas = partidas.FindAll(p => !p.terminada);

            partidasNoAcabadas.Sort((p1, p2) => p2.creacion.CompareTo(p1.creacion));

            Console.CursorVisible = false;

            int centroX = Dibujar.X + Dibujar.AnchuraRectangulo / 2 - 1;
            int centroY = Dibujar.Y + Dibujar.AlturaRectangulo / 2 - 1;

            int opcion = 0;
            bool salir = false;
            bool salir2 = false;

            Dibujar.DibujarSpriteCentrado(centroX - 55, centroY - 15, "Selecciona la partida a continuar: ");

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            while (!salir && !salir2)
            {
                int espaciadoVertical = 10;
                int maxPartidasMostrar = Math.Min(10, partidasNoAcabadas.Count());
                int maxMostrar = maxPartidasMostrar + 1;

                for (int i = 0; i < maxMostrar; i++)
                {
                    string simb = "\u2192";
                    if (opcion == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    if (i < maxPartidasMostrar)
                    {
                        Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical, $"{i + 1}. " + partidasNoAcabadas[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Dibujar.DibujarSpriteCentrado(centroX - 1, centroY - espaciadoVertical + 4, simb + " Salir");
                        Console.ResetColor();
                    }

                    espaciadoVertical -= 2;

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
                            if (opcion > 0)
                                opcion--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (opcion < maxMostrar - 1)
                                opcion++;
                            break;
                        case ConsoleKey.Enter:
                            if (opcion == maxMostrar - 1)
                            {
                                salir = true;
                            }
                            else
                            {
                                salir2 = true;
                            }
                            break;
                    }
                }
            }
            int partidaAContinuar = opcion;
            if (salir)
            {
                return null;
            }
            return partidasNoAcabadas[partidaAContinuar];
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
            bool victoriaCombateFinal = false;
            bool combateFinalTerminado = false;
            while (tecla.Key != ConsoleKey.Escape && !combateFinalTerminado)
            {
                Dibujar.DibujarMapa(mapa, partida.jugador.x, partida.jugador.y);
                if (Console.KeyAvailable == true)
                {
                    tecla = Console.ReadKey(true);
                    bool seMovio = false;
                    
                    int x = 0;
                    int y = 0;

                    switch (tecla.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            char cUp = mapa[partida.jugador.x, partida.jugador.y - 1];
                            if (cUp != 'A' && cUp != 'J')
                            {
                                partida.jugador.y -= 1;
                                seMovio = true;
                            }
                            else
                            {
                                if(cUp == 'J' && partida.jugador.Inventario.ContieneObjetoClave())
                                    y -= 1;
                            }
                                break;
                        case ConsoleKey.RightArrow:
                            char cDown = mapa[partida.jugador.x, partida.jugador.y + 1];
                            if (cDown != 'A' && cDown != 'J')
                            {
                                partida.jugador.y += 1;
                                seMovio = true;
                            }
                            else
                            {
                                if (cDown == 'J' && partida.jugador.Inventario.ContieneObjetoClave())
                                    y += 1;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            char cLeft = mapa[partida.jugador.x - 1, partida.jugador.y];
                            if (cLeft != 'A' && cLeft != 'J')
                            {
                                partida.jugador.x -= 1;
                                seMovio = true;
                            }
                            else
                            {
                                if (cLeft == 'J' && partida.jugador.Inventario.ContieneObjetoClave())
                                    x -= 1;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            char cRight = mapa[partida.jugador.x + 1, partida.jugador.y];
                            if (cRight != 'A' && cRight != 'J')
                            {
                                partida.jugador.x += 1;
                                seMovio = true;
                            }
                            else
                            {
                                if (cRight == 'J' && partida.jugador.Inventario.ContieneObjetoClave())
                                    x += 1;
                            }
                            break;
                    }


                    if (seMovio && ProbabilidadCombate())
                    {
                        CombateAleatorio combate = new CombateAleatorio(partida);
                        combate.IniciarCombate();

                        if (partida.jugador.Vida <= 0)
                            tecla = new ConsoleKeyInfo('\u001b', ConsoleKey.Escape, false, false, false);
                    }

                    if (mapa[partida.jugador.x, partida.jugador.y] == 'C')
                    {
                        partida.GuardarPartida();
                    }

                    if (mapa[partida.jugador.x + x, partida.jugador.y + y] == 'J')
                    {
                        if (partida.jugador.Inventario.ContieneObjetoClave())
                        {
                            ProyectoRPG.Combate.Combate combateFinal = new ProyectoRPG.Combate.Combate(partida, ProyectoRPG.Combate.Combate.demonio);
                            victoriaCombateFinal = combateFinal.EmpezarCombate();
                            combateFinalTerminado = true;
                            if (!victoriaCombateFinal)
                            {
                                Dibujar.LimpiarPantalla();
                                Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2), "Has sido derrotado por el demonio... volverás al menú principal");
                                Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 2, "Pulsa ENTER para salir");
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                            }
                        }
                        else
                        {
                            Dibujar.LimpiarPantalla();
                            Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2), "La pared está muy empinada, no podrás subir sin un pico.");
                            Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 2, "Pulsa ENTER para continuar");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter){ }
                            Dibujar.LimpiarPantalla();
                        }
                    }
                    if (victoriaCombateFinal)
                    {
                        Dibujar.LimpiarPantalla();
                        Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2), "¡Felicidades! Has derrotado al demonio y completado el juego.");
                        Dibujar.DibujarSpriteCentrado(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 2, "Pulsa ENTER para continuar");
                        partida.puntuacion += 100;
                        partida.terminada = true;
                        partida.GuardarPartida();
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        Dibujar.LimpiarPantalla();
                        return;
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
                IgnoreReadOnlyProperties = false,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
            {
                typeInfo =>
                {
                    if (typeInfo.Type == typeof(Jugador))
                    {
                        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            DerivedTypes =
                            {
                                new JsonDerivedType(typeof(Caballero), "caballero"),
                                new JsonDerivedType(typeof(Mago), "mago"),
                                new JsonDerivedType(typeof(Elfo), "elfo"),
                                new JsonDerivedType(typeof(Picaro), "picaro")
                            }
                        };
                    }
                }
            }
                }
            };

            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText("./../../../Recursos/jugadores/" + NombreArchivo(), json);
        }


        public static List<Partida> CargarPartidas()
        {
            string carpeta = "./../../../Recursos/jugadores/";
            var partidas = new List<Partida>();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyProperties = false,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
            {
                typeInfo =>
                {
                    if (typeInfo.Type == typeof(Jugador))
                    {
                        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            DerivedTypes =
                            {
                                new JsonDerivedType(typeof(Caballero), "caballero"),
                                new JsonDerivedType(typeof(Mago), "mago"),
                                new JsonDerivedType(typeof(Elfo), "elfo"),
                                new JsonDerivedType(typeof(Picaro), "picaro")
                            }
                        };
                    }
                }
            }
                }
            };

            foreach (var archivo in Directory.GetFiles(carpeta, "*.json"))
            {
                try
                {
                    string json = File.ReadAllText(archivo);
                    var partida = JsonSerializer.Deserialize<Partida>(json, options);
                    if (partida != null)
                        partidas.Add(partida);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar {archivo}: {ex.Message}");
                }
            }

            return partidas;
        }

        public override string ToString()
        {
            return $"{jugador.Nombre} ({jugador.GetType().Name}) - {puntuacion} pts";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoRPG
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
            if(escritoMal)
            {
                Console.SetCursorPosition(Dibujar.X + 2, (Dibujar.AlturaRectangulo + Dibujar.Y) / 2 - 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: El nombre debe estar entre 1 y 30 caracteres y no debe usar caracteres especiales o estar ya registrado.");
                Console.ResetColor();
            }

            Console.SetCursorPosition(Dibujar.X + 2, (Dibujar.AlturaRectangulo + Dibujar.Y) / 2 + 1);
            Console.Write("Nombre del jugador (30 caracteres): ");
            return Console.ReadLine();
        }

        private static bool NombreUsuarioValido(string nombre)
        {
            string cadenaCaracteresNoValidos = "ºª\\!|\"@·#$~%€&¬/()='?¡¿`^[+*]´¨{ç},;.:-_<>";

            List<FileInfo> ficheros = new List<FileInfo>(new DirectoryInfo("./jugadores").GetFiles());
            List<string> nombresArchivos = ficheros.Select(f => f.Name).ToList();

            bool caracterNoValido = false;
            for(int i=0;i<cadenaCaracteresNoValidos.Length && !caracterNoValido;i++)
            {
                caracterNoValido = nombre.Contains(cadenaCaracteresNoValidos[i]);
            }

            if(nombre.Length > 30)
            {
                Console.Clear();
                Dibujar.DibujarRectanguloPrincipal();
            }

            return nombre.Length == 0 || nombre.Length > 30 || caracterNoValido || nombresArchivos.Contains($"{nombre}.json");
        }

        private static void ClaseElegida() // Debería devolver algo...
        {
            string[] opciones = ["Mago", "Caballero", "Elfo", "Picaro"];
            string[] textosPersonajes = ["Un personaje que vive en una constante batalla mental",
                                         "No conocerás a ser más noble, pero eso no quita que tenga sus vicios",
                                         "El guardian de los bosques y el un gran guerrero a la distancia",
                                         "Un humano con una afición por conseguir de formas inapropiadas cosas que no son suyas"];
            int indice = 0;

            int xSprite = (int)(Dibujar.AnchuraRectangulo / 1.5) - 5;
            int ySprite = Dibujar.Y + (Dibujar.AlturaRectangulo/4) - 5;

            ConsoleKeyInfo tecla = new ConsoleKeyInfo();

            while (tecla.Key != ConsoleKey.Enter)
            {
                int espaciadoVertical = -2;
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

                    Dibujar.DibujarSpriteCentrado(Dibujar.AnchuraRectangulo / 4 - 1, Dibujar.AlturaRectangulo / 4 - espaciadoVertical, opciones[i]);
                    espaciadoVertical -= 8;

                    if (indice == i)
                    {
                        Console.ResetColor();
                    }

                    switch(indice)
                    {
                        case 0:
                            Dibujar.DibujarSpriteNormal(xSprite, ySprite, Sprites.Mago);
                            break;
                        case 1:
                            Dibujar.DibujarSpriteNormal(xSprite, ySprite + 2, Sprites.Caballero);
                            break;
                        case 2:
                            Dibujar.DibujarSpriteNormal(xSprite + 8, ySprite + 1, Sprites.Elfo);
                            break;
                        case 3:
                            Dibujar.DibujarSpriteNormal(xSprite + 3, ySprite + 1, Sprites.Picaro);
                            break;
                    }

                    Dibujar.DibujarSpriteCentrado(xSprite + Dibujar.X, Dibujar.AlturaRectangulo + Dibujar.Y - 10, textosPersonajes[indice]);
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
                    }
                }
            }
            Console.CursorVisible = true;
        }

        public static Partida NuevaPartida()
        {
            string nombreUsuario = NombreElegido();
            ClaseElegida();
            // Cinematica
            // Minijuego
            // Se crea el OBJETO personaje y se devuelve esta partida con ese personaje dentro

            return new Partida();
        }

        public string NombreArchivo()
        {
            return $"{jugador.GetNombre()}.json";
        }

        public void GuardarPartida()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(this.NombreArchivo(), json);
            Console.WriteLine("Catálogo guardado.");
        }

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

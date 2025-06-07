using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Interfaz;

namespace ProyectoRPG.Minijuegos
{
    
    public class MinijuegoAhorcado : Minijuego
    {
        List<string> palabras = new List<string>();
        int palabra;
        string estado;
        int vidas;
        int maxVidas;
        bool ganado;
        public MinijuegoAhorcado()
        {
            RellenarLista();
            palabra = 0;
            estado = "";
            vidas = 0;
            maxVidas = 6;
            ganado = false;
        }

        public List<string> GetPalabras() { return palabras; }
        public int GetPalabra() { return palabra; }

        public List<string> RellenarLista()
        {
            palabras.Add("tomate");
            palabras.Add("ballena");
            palabras.Add("berenjena");
            palabras.Add("hipopotamo");
            palabras.Add("sandia");
            palabras.Add("lechuga");
            return palabras;
        }
        public string GetEstado()
        {
            string res = "";
            switch (vidas)
            {
                case 0:
                    break;
                case 1:
                    res = "cabeza";
                    break;
                case 2:
                    res = "torso";
                    break;
                case 3:
                    res = "brazoizq";
                    break;
                case 4:
                    res = "brazoder";
                    break;
                case 5:
                    res = "piernaizq";
                    break;
                case 6:
                    res = "piernader";
                    break;
                default:
                    break;
            }
            return res;
        }
        public int GetVidas() { return vidas; }

        public void SetVidas(int vidas) { this.vidas = vidas; }
        public void SetPalabras(List<string> palabras) { this.palabras = palabras; }
        public void SetPalabra(int palabra) { this.palabra = palabra; }
        public void Add(string palabra) { palabras.Add(palabra); }
        public void AddDaño() { vidas++; }

        public void DibujarPalabra()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 3);
            Console.WriteLine("Palabra: " + estado);
        }

        public void DibujarAhorcado(string estado)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 11);

            Console.WriteLine("____");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 9);
            Console.WriteLine("|");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 8);
            Console.WriteLine("|");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 7);
            Console.WriteLine("|");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 6);
            Console.WriteLine("_");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 10);
            switch (estado)
            {
                case "cabeza":
                    Console.Write("  O\n");
                    break;
                case "torso":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 9);
                    Console.Write("  |\n");
                    break;
                case "brazoizq":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 9);
                    Console.Write(" /|\n");
                    break;
                case "brazoder":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 9);
                    Console.Write(" /|\\\n");
                    break;
                case "piernaizq":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 9);
                    Console.Write(" /|\\\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 8);
                    Console.Write(" /\n");
                    break;
                case "piernader":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 9);
                    Console.Write(" /|\\\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 8);
                    Console.Write(" / \\\n");
                    break;
            }
        }

        public bool HasGanado()
        {
            return estado == palabras[palabra];
        }

        public bool EstasAhorcado()
        {
            return vidas == maxVidas;
        }

        public void ActualizarPalabra()
        {
            bool acierto = false;

            // Limpia la línea de entrada
            int inputX = Console.WindowWidth / 2 - 9;
            int inputY = Console.WindowHeight / 2 + 12;
            Console.SetCursorPosition(inputX, inputY);
            Console.Write(new string(' ', 40)); // Borra línea anterior
            Console.SetCursorPosition(inputX, inputY);
            Console.Write("Introduce una letra: ");

            string entrada = Console.ReadLine();

            // Limpia mensaje de error anterior (si lo hubo)
            Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 32);
            Console.Write(new string(' ', 60)); // Borra error anterior

            if (char.TryParse(entrada, out char letraUsuario) && char.IsLetter(letraUsuario))
            {
                letraUsuario = char.ToLower(letraUsuario); // Por si se ingresa en mayúscula

                for (int i = 0; i < palabras[palabra].Length; i++)
                {
                    if (palabras[palabra][i] == letraUsuario)
                    {
                        estado = estado.Remove(i, 1).Insert(i, letraUsuario.ToString());
                        acierto = true;
                    }
                }

                if (!acierto)
                {
                    AddDaño();
                    ActualizarAhorcado();
                }
            }
            else
            {
                Console.SetCursorPosition(Dibujar.x + 4, Dibujar.alturaRectangulo - 32);
                Console.WriteLine("¡ERROR! Debes introducir solo una letra.".PadRight(60));
            }
        }


        public void ActualizarAhorcado()
        {
            DibujarAhorcado(GetEstado());
        }

        public bool Jugar()
        {
            Random generator = new Random();
            palabra = generator.Next(0, palabras.Count);
            estado = "";
            for (int i = 0; i < palabras[palabra].Length; i++)
            {
                estado += "_";
            }

            while (!HasGanado() && !EstasAhorcado())
            {
                DibujarPalabra();
                DibujarAhorcado(GetEstado());

                ActualizarPalabra();
                ActualizarAhorcado();
            }

            if (HasGanado())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2 + 14);
                Console.WriteLine($"¡Has ganado! La palabra era: {palabras[palabra]}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2 + 14);
                Console.WriteLine($"¡Has perdido! La palabra era: {palabras[palabra]}");
            }

            Console.CursorVisible = false;
            Console.ResetColor();

            for(int i=8; i>0; i--)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2 + 16);
                Console.WriteLine($"Comenzando el juego en {i}...");
                Thread.Sleep(1000);
            }

            return HasGanado();
        }
    }
}
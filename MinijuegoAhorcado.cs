using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG;

namespace ProyectoRPG
{
    
    internal class Ahorcado:Minijuego
    {
        List<string> palabras = new List<string>();
        int palabra;
        string estado;
        int vidas;
        int maxVidas;
        bool ganado;
        public Ahorcado(List<string> palabras)
        {
            this.palabras = palabras;
            palabra = 0;
            estado = "";
            vidas = 0;
            maxVidas = 6;
            ganado = false;
        }

        public List<string> GetPalabras() { return palabras; }
        public int GetPalabra() { return palabra; }
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
            Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) + 10);
            Console.WriteLine("Palabra: " + estado);
        }

        public void DibujarAhorcado(string estado)
        {
            Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) - 1);

            Console.WriteLine("____");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) + 1);
            Console.WriteLine("|");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) + 2);
            Console.WriteLine("|");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) + 3);
            Console.WriteLine("|");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) + 4);
            Console.WriteLine("_");
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            switch (estado)
            {
                case "cabeza":
                    Console.Write("  O\n");
                    break;
                case "torso":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
                    Console.Write("  |\n");
                    break;
                case "brazoizq":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
                    Console.Write(" /|\n");
                    break;
                case "brazoder":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
                    Console.Write(" /|\\\n");
                    break;
                case "piernaizq":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
                    Console.Write(" /|\\\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 2);
                    Console.Write(" /\n");
                    break;
                case "piernader":
                    Console.Write("  O\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
                    Console.Write(" /|\\\n");
                    Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 2);
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
            Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) + 15);
            Console.Write("Introduce una letra: ");
            char letraUsuario = Convert.ToChar(Console.ReadLine());
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

        public void ActualizarAhorcado()
        {
            DibujarAhorcado(GetEstado());
        }

        public void Jugar()
        {
            Random generator = new Random();
            palabra = generator.Next(0, palabras.Count() + 1);
            estado = "";
            for (int i = 0; i < palabras[palabra].Length; i++)
            {
                estado += "_";
            }

            while (!HasGanado() && !EstasAhorcado())
            {
                Console.Clear();
                DibujarPalabra();
                DibujarAhorcado(GetEstado());

                ActualizarPalabra();
                ActualizarAhorcado();
            }
            if (HasGanado())
            {
                Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) + 20);
                Console.WriteLine($"¡Has ganado! La palabra era: {palabras[palabra]}");
            }
            else
            {
                Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) + 20);
                Console.WriteLine($"¡Has perdido! La palabra era: {palabras[palabra]}");
            }
        }
    }
}
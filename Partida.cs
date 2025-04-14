using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        private static string PedirNombreUsuario(bool escritoMal)
        {
            if(escritoMal)
            {
                Console.SetCursorPosition(Dibujar.X + 2, (Dibujar.AlturaRectangulo + Dibujar.Y) / 2 - 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: El nombre debe estar entre 1 y 30 caracteres y no debe usar caracteres especiales.");
                Console.ResetColor();
            }

            Console.SetCursorPosition(Dibujar.X + 2, (Dibujar.AlturaRectangulo + Dibujar.Y) / 2 + 1);
            Console.Write("Nombre del jugador (30 caracteres): ");
            return Console.ReadLine();
        }

        private static bool NombreUsuarioValido(string nombre)
        {
            string caracteresNoValidos = "ºª\\!|\"@·#$~%€&¬/()='?¡¿`^[+*]´¨{ç},;.:-_<>";
            bool resultado = nombre.Length == 0 || nombre.Length > 30;

            if(!resultado)
            {
                for(int i=0;i<caracteresNoValidos.Length && resultado;i++)
                {
                    resultado = nombre.Contains(caracteresNoValidos[i]);
                }
            }

            return resultado;
        }

        public static Partida NuevaPartida()
        {   
            string nombreUsuario = "";
            bool escritoMal = false;

            do
            {
                if(escritoMal)
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

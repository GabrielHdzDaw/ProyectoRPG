using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Partida NuevaPartida()
        {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Personajes
{
    public abstract class Personaje
    {
        public string Nombre { get; set; }
        public string Sprite { get; set; }
        public int Vida { get; set; }
        public int VidaMaxima { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Velocidad { get; set; }


        public Personaje() { }

        public Personaje(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad)
        {
            this.Nombre = nombre;
            this.Sprite = sprite;
            this.Vida = vida;
            VidaMaxima = vida;
            this.Ataque = ataque;
            this.Defensa = defensa;
            this.Velocidad = velocidad;
            
        }

        public void RecibirDaño(int daño)
        {
            Vida = Math.Max(0, Vida - daño);
        }

        public void Curar(int cantidad)
        {
            Vida = Math.Min(VidaMaxima, Vida + cantidad);
        }

        public bool EstaMuerto()
        {
            return Vida <= 0;
        }


        public override string ToString()
        {
            return $"Nombre: {Nombre}, Vida: {Vida}/{VidaMaxima}, Ataque: {Ataque}, Defensa: {Defensa}, Velocidad: {Velocidad}";
        }
    }
}

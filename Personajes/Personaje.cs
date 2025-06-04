using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Personajes
{
    internal abstract class Personaje
    {
        string nombre;
        string sprite;
        int vida;
        int vidaMaxima;
        int ataque;
        int defensa;
        int velocidad;
        

        public Personaje(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad)
        {
            this.nombre = nombre;
            this.sprite = sprite;
            this.vida = vida;
            vidaMaxima = vida;
            this.ataque = ataque;
            this.defensa = defensa;
            this.velocidad = velocidad;
            
        }

        public string GetNombre()
        {
            return nombre;
        }

        public string GetSprite()
        {
            return sprite;
        }

       

        public int GetVida()
        {
            return vida;
        }

        public int GetVidaMaxima()
        {
            return vidaMaxima;
        }

        public int GetAtaque()
        {
            return ataque;
        }

        public int GetDefensa()
        {
            return defensa;
        }

        public int GetVelocidad()
        {
            return velocidad;
        }

        public void SetNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public void SetSprite(string sprite)
        {
            this.sprite = sprite;
        }


        public void SetVida(int vida)
        {
            this.vida = vida;
        }

        public void SetVidaMaxima(int vidaMaxima)
        {
            this.vidaMaxima = vidaMaxima;
        }

        public void SetAtaque(int ataque)
        {
            this.ataque = ataque;
        }


        public void SetDefensa(int defensa)
        {
            this.defensa = defensa;
        }

        public void SetVelocidad(int velocidad)
        {
            this.velocidad = velocidad;
        }

        

        public void RecibirDaño(int daño)
        {
            vida = Math.Max(0, vida - daño);
        }

        public void Curar(int cantidad)
        {
            vida = Math.Min(vidaMaxima, vida + cantidad);
        }

        public bool EstaMuerto()
        {
            return vida <= 0;
        }


        public override string ToString()
        {
            return $"Nombre: {nombre}, Vida: {vida}/{vidaMaxima}, Ataque: {ataque}, Defensa: {defensa}, Velocidad: {velocidad}";
        }
    }
}

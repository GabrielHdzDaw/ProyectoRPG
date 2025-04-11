using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal abstract class Personaje
    {
        string nombre;
        string sprite;
        List<Item> inventario;
        int vida;
        int ataque;
        int defensa;
        int velocidad;

        public Personaje(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad)
        {
            this.nombre = nombre;
            this.sprite = sprite;
            inventario = new List<Item>();
            this.vida = vida;
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

        public List<Item> GetInventario()
        {
            return inventario;
        }

        public int GetVida()
        {
            return vida;
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

        public void AgregarItem(Item item)
        {
            inventario.Add(item);
        }

        public void EliminarItem(Item item)
        {
            inventario.Remove(item);
        }




        public override string ToString()
        {
            return $"Nombre: {nombre}, Vida: {vida}, Ataque: {ataque}, Defensa: {defensa}, Velocidad: {velocidad}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Combate;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Personajes
{
    internal class Jugador:Personaje
    {
        string nombre { get; set; }
        string sprite { get; set; }
        int vida { get; set; }
        int ataque { get; set; }
        int defensa { get; set; }
        int velocidad { get; set; }
        public List<Ataque> Ataques { get; protected set; }
        protected InventarioPersonaje inventario;
        Arma? armaEquipada;

        public int AtaqueTotal
        {
            get
            {
                return ataque + (armaEquipada?.dano ?? 0);
            }
        }

        public int AtaqueCriticoTotal
        {
            get
            {
                return ataque + (armaEquipada?.danoCritico ?? 0);
            }
        }


        public Jugador(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad) : base(nombre, sprite, vida, ataque, defensa, velocidad)
        {
            Ataques = new List<Ataque>();
            inventario = new InventarioPersonaje();
            inventario.AgregarObjeto(new Pocion(20));
        }
        
        public List<Ataque> GetAtaques()
        {
            return Ataques;
        }

        public InventarioPersonaje GetInventario()
        {
            return inventario;
        }

        public Arma? GetArma()
        {
            return armaEquipada;
        }

        public void SetArma(Arma arma)
        {
            if (inventario.ContieneObjeto(arma))
            {
                armaEquipada = arma;
            }
            else
            {
                throw new InvalidOperationException("El arma no está en el inventario.");
            }
        }

        public void AgregarItem(Item item)
        {
            inventario.AgregarObjeto(item);
        }

        public void EliminarItem(Item item)
        {
            inventario.EliminarObjeto(item);
        }

        public void UsarPocion(Pocion pocion)
        {
            if (inventario.ContieneObjeto(pocion))
            {
                int nuevaVida = GetVida() + pocion.curacion;

                if (nuevaVida > GetVidaMaxima())
                {
                    nuevaVida = GetVidaMaxima();
                }

                SetVida(nuevaVida);
                inventario.EliminarObjeto(pocion);
            }
            else
            {
                throw new InvalidOperationException("La poción no está en el inventario.");
            }
        }

        public override string ToString()
        {
            return "Jugador " + base.ToString();
        }
    }
}

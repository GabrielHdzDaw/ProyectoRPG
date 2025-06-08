using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRPG.Combate;
using ProyectoRPG.Inventario;

namespace ProyectoRPG.Personajes
{
    public class Jugador:Personaje
    {
        public int x { get; set; }
        public int y { get; set; }
        public List<Ataque> Ataques { get; set; }
        public InventarioPersonaje Inventario { get; set; }
        public Arma ArmaEquipada { get; set; }

        public int AtaqueTotal
        {
            get
            {
                return Ataque + (ArmaEquipada?.Dano ?? 0);
            }
        }

        public int AtaqueCriticoTotal
        {
            get
            {
                return Ataque + (ArmaEquipada?.DanoCritico ?? 0);
            }
        }


        public Jugador(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad) : base(nombre, sprite, vida, ataque, defensa, velocidad)
        {
            Ataques = new List<Ataque>();
            Inventario = new InventarioPersonaje();
            Inventario.AgregarObjeto(new Pocion(20));
            x = 66;
            y = 127;
        }
        
        public List<Ataque> GetAtaques()
        {
            return Ataques;
        }

        public InventarioPersonaje GetInventario()
        {
            return Inventario;
        }

        public Arma? GetArma()
        {
            return ArmaEquipada;
        }

        public void SetArma(Arma arma)
        {
            if (Inventario.ContieneObjeto(arma))
            {
                ArmaEquipada = arma;
            }
            else
            {
                throw new InvalidOperationException("El arma no está en el inventario.");
            }
        }

        public void AgregarItem(Item item)
        {
            Inventario.AgregarObjeto(item);
        }

        public void EliminarItem(Item item)
        {
            Inventario.EliminarObjeto(item);
        }

        public void UsarPocion(Pocion pocion)
        {
            if (Inventario.ContieneObjeto(pocion))
            {
                int nuevaVida = Vida + pocion.curacion;

                if (nuevaVida > VidaMaxima)
                {
                    nuevaVida = VidaMaxima;
                }

                Vida = nuevaVida;
                Inventario.EliminarObjeto(pocion);
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

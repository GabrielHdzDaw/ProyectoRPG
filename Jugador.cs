﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Jugador:Personaje
    {
        public Jugador(string nombre, string sprite, int vida, int ataque, int defensa, int velocidad) : base(nombre, sprite, vida, ataque, defensa, velocidad)
        {
        }

        public override string ToString()
        {
            return "Jugador " + base.ToString();
        }
    }
}

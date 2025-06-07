using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Minijuegos
{
    public class Minijuego
    {
        protected int puntuacionMinijuego;

        public Minijuego()
        {
            PuntuacionMinijuego = 0;
        }

        protected int PuntuacionMinijuego { get => puntuacionMinijuego; set => puntuacionMinijuego = value; }
    }
}
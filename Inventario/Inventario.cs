using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    internal class InventarioPersonaje
    {
        public List<Pocion> Pociones { get; private set; }
        public List<Arma> Armas { get; private set; }
        public List<Armadura> Armaduras { get; private set; }
        public List<ObjetoClave> ObjetosClave { get; private set; }

        public InventarioPersonaje()
        {
            Pociones = new List<Pocion>();
            Armas = new List<Arma>();
            Armaduras = new List<Armadura>();
            ObjetosClave = new List<ObjetoClave>();
        }

        public void AgregarObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                Pociones.Add(pocion);
            else if (objeto is Arma arma)
                Armas.Add(arma);
            else if (objeto is Armadura armadura)
                Armaduras.Add(armadura);
        }

        public void EliminarObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                Pociones.Remove(pocion);
            else if (objeto is Arma arma)
                Armas.Remove(arma);
            else if (objeto is Armadura armadura)
                Armaduras.Remove(armadura);
        }

        public bool ContieneObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                return Pociones.Contains(pocion);
            else if (objeto is Arma arma)
                return Armas.Contains(arma);
            else if (objeto is Armadura armadura)
                return Armaduras.Contains(armadura);
            return false;
        }

        public override string ToString()
        {
            var todos = new List<string>();
            todos.AddRange(Pociones.Select(p => p.ToString()));
            todos.AddRange(Armas.Select(a => a.ToString()));
            todos.AddRange(Armaduras.Select(a => a.ToString()));
            todos.AddRange(ObjetosClave.Select(o => o.ToString()));
            return string.Join(", ", todos);
        }
    }
}

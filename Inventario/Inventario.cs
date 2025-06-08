using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Inventario
{
    public class InventarioPersonaje
    {
        public List<Pocion> Pociones { get; set; }
        public List<Arma> Armas { get; set; }
        public List<ObjetoClave> ObjetosClave { get; set; }

        public InventarioPersonaje()
        {
            Pociones = new List<Pocion>();
            Armas = new List<Arma>();
            ObjetosClave = new List<ObjetoClave>();
        }

        public void AgregarObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                Pociones.Add(pocion);
            else if (objeto is Arma arma)
                Armas.Add(arma);
            
        }

        public void EliminarObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                Pociones.Remove(pocion);
            else if (objeto is Arma arma)
                Armas.Remove(arma);
           
        }

        public bool ContieneObjeto(Item objeto)
        {
            if (objeto is Pocion pocion)
                return Pociones.Contains(pocion);
            else if (objeto is Arma arma)
                return Armas.Contains(arma);
           
            return false;
        }

        public void AgregarObjetoClave(ObjetoClave objetoClave)
        {
            ObjetosClave.Add(objetoClave);
        }

        public bool ContieneObjetoClave()
        {
            return ObjetosClave.Any(o => o.Nombre == "Pico de escalada");
        }

        public override string ToString()
        {
            var todos = new List<string>();
            todos.AddRange(Pociones.Select(p => p.ToString()));
            todos.AddRange(Armas.Select(a => a.ToString()));
           
            todos.AddRange(ObjetosClave.Select(o => o.ToString()));
            return string.Join(", ", todos);
        }
    }
}

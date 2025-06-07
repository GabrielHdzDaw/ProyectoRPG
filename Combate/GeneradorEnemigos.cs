using System;
using ProyectoRPG.Personajes;
using ProyectoRPG.Recursos;

namespace ProyectoRPG.Combate
{
    public static class GeneradorEnemigos
    {
        public static Enemigo EnemigoAleatorio()
        {
            Enemigo[] enemigos = new Enemigo[]
            {
                new Enemigo("Fantasma", Sprites.Fantasma, 80, 18, 12, 10),
                new Enemigo("Fantasmikos", Sprites.Fantasmicos, 100, 18, 12, 12),
                new Enemigo("Esqueleto", Sprites.Esqueleto, 80, 15, 10, 6),
                new Enemigo("Centauro", Sprites.Centauro, 120, 20, 15, 15),
                new Enemigo("Caballero Chepa", Sprites.CaballeroChepa, 90, 20, 15, 4),
                new Enemigo("Goblin", Sprites.Goblin, 100, 20, 15, 10),
                new Enemigo("Caballero de Élite", Sprites.CaballeroElite, 130, 25, 20, 20),
            };
            Random random = new Random();
            int indice = random.Next(0, enemigos.Length);
            return enemigos[indice];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
{
    internal class Transicion
    {
        public static void DibujarTransicion1()
        {
            string[] patterns =
            {
                new string(' ', Console.WindowWidth),
                new string('░', Console.WindowWidth),
                new string('░', Console.WindowWidth),

                new string('▒', Console.WindowWidth),
                new string('▒', Console.WindowWidth),

                new string('▓', Console.WindowWidth),
                new string('▓', Console.WindowWidth),

                new string('█', Console.WindowWidth)
            };

            string whiteLine = new string('█', Console.WindowWidth);
            int whiteLines = Console.WindowHeight - 5;

            for (int i = 0; i < Console.WindowHeight + (patterns.Length * 2) + whiteLines; i++)
            {
                Console.CursorVisible = false;
                for (int j = 0; j < patterns.Length; j++)
                {
                    int layerPos = i - j;
                    if (layerPos >= 0 && layerPos < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(0, layerPos);
                        Console.Write(patterns[j]);
                    }
                }

                for (int w = 0; w < whiteLines; w++)
                {
                    int whitePos = i - patterns.Length - w;
                    if (whitePos >= 0 && whitePos < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(0, whitePos);
                        Console.Write(whiteLine);
                    }
                }

                for (int j = 0; j < patterns.Length; j++)
                {
                    int layerPos = i - patterns.Length - whiteLines - j;
                    if (layerPos >= 0 && layerPos < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(0, layerPos);
                        Console.Write(patterns[patterns.Length - 1 - j]);
                    }
                }

                Thread.Sleep(1);
                if (i > Console.WindowHeight / 2)
                {
                    Console.Clear(); //Clean residual lines
                }
            }
            Console.Clear();
        }
    }
}

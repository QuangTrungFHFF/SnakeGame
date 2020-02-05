using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Game
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Game() : this(20,30)
        {
        }
        public Game(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Render();
        }

        private void Render()
        {
            Console.WriteLine($"Map: {this.Height} x {this.Width}");

            var map = this.DrawMap();
            
            this.PrintMap(map);

        }
        /// <summary>
        /// Draw the walls, fill the field with ""
        /// </summary>
        /// <returns></returns>
        private string[,] DrawMap()
        {
            var map = new string[this.Height, this.Width];

            //Horizontal walls
            for(int j =0; j< this.Width;j++)
            {
                map[0, j] = "*";
                map[this.Height - 1, j] = "*";
            }

            //Vertical walls
            for (int i =1; i< this.Height-1;i++ )
            {
                map[i, 0] = "*";
                map[i, this.Width-1] = "*";
            }

            //Field
            for (int i = 1; i < this.Height-1; i++)
            {
                for (int j = 1; j < this.Width-1; j++)
                {
                    map[i, j] = "";
                }
            }
            return map;
        }
        public void PrintMap(string[,] map)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Console.Write(map[i, j].PadLeft(1)+ " ");
                }
                Console.WriteLine();
            }
        }
        
    }
}

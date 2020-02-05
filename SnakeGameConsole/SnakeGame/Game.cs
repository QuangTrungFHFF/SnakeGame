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
        private int[,] DrawMap()
        {
            var map = new int[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    map[i, j] = 0;
                }
            }
            return map;
        }
        public void PrintMap(int[,] map)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Console.Write(map[i, j]+ " ");
                }
                Console.WriteLine();
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Game
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public string[,] Map { get; private set; }

        public Game() : this(20,30)
        {
        }
        public Game(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Render();            
            
        }
        private void Play()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                while(!isValidKey(key))
                {
                    key = Console.ReadKey();
                }


            } while (key.Key != ConsoleKey.Escape);
        }

        private bool isValidKey(ConsoleKeyInfo key)
        {
            var validKeys = new List<ConsoleKey>() {ConsoleKey.Enter,ConsoleKey.W,ConsoleKey.A, ConsoleKey.D,ConsoleKey.S,ConsoleKey.Escape};
            if(validKeys.Contains(key.Key))
            {
                return true;
            }
            return false;
        }
        private void Render()
        {
            Console.WriteLine($"Map: {this.Height} x {this.Width}");

            var snake = new Snake(new List<(int, int)>() { (3, 3),(3,2),(3,1)},Direction.Right);
            this.DrawMap(snake);
            this.PrintMap();
        }
        
        /// <summary>
        /// Draw the walls, fill the field with ""
        /// </summary>
        /// <returns></returns>
        private void DrawMap(Snake snake)
        {
            this.Map = new string[this.Height, this.Width];

            //Horizontal walls
            for(int j =0; j< this.Width;j++)
            {
                this.Map[0, j] = "*";
                this.Map[this.Height - 1, j] = "*";
            }

            //Vertical walls
            for (int i =1; i< this.Height-1;i++ )
            {
                this.Map[i, 0] = "*";
                this.Map[i, this.Width-1] = "*";
            }

            //Field
            for (int i = 1; i < this.Height-1; i++)
            {
                for (int j = 1; j < this.Width-1; j++)
                {
                    if(snake.SnakeBody.Contains((i,j)))
                    {
                        this.Map[i, j] = "0";
                    }
                    else
                    {
                        this.Map[i, j] = "";
                    }
                }
            }

            //Snake head
            this.Map[snake.Head.Item1, snake.Head.Item2] = "X";
        }
        public void PrintMap()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Console.Write(this.Map[i, j].PadLeft(1)+ " ");
                }
                Console.WriteLine();
            }
        }
        
    }
}

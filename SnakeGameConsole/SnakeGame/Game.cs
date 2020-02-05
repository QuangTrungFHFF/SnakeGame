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
        public Snake Snake { get; private set; }

        public Game() : this(20,30)
        {
        }
        public Game(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Snake = new Snake(new List<(int, int)>() { (3, 3), (3, 2), (3, 1) }, Direction.Right);
            this.Render();
            this.Play();
            
        }
        private void Play()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                while(!isValidKey(key))
                {
                    key = Console.ReadKey(true);
                }

                if(key.Key != ConsoleKey.Escape)
                {
                    var direction = GetDirectionFromKey(key.Key);
                    this.Move(direction);
                    Console.Clear();
                    this.Render();
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
        private Direction GetDirectionFromKey(ConsoleKey key)
        {
            Direction direction;
            switch (key)
            {
                case ConsoleKey.Enter:
                    direction = this.Snake.Direction;
                    break;
                case ConsoleKey.A:
                    if(this.Snake.Direction == Direction.Right)
                    {
                        direction = this.Snake.Direction;                        
                    }
                    else
                    {
                        direction = Direction.Left;
                    }                    
                    break;
                case ConsoleKey.D:
                    if (this.Snake.Direction == Direction.Left)
                    {
                        direction = this.Snake.Direction;
                    }
                    else
                    {
                        direction = Direction.Right;
                    }                    
                    break;
                case ConsoleKey.S:
                    if (this.Snake.Direction == Direction.Up)
                    {
                        direction = this.Snake.Direction;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }                    
                    break;
                case ConsoleKey.W:
                    if (this.Snake.Direction == Direction.Down)
                    {
                        direction = this.Snake.Direction;
                    }
                    else
                    {
                        direction = Direction.Up;
                    }                    
                    break;
                default:
                    direction = this.Snake.Direction;
                    break;
            }
            return direction;            
        }
        private void Move(Direction direction)
        {
            (int, int) currentPos = this.Snake.Head;
            
            switch (direction)
            {
                case Direction.Up:                    
                    (int,int)nextPos = (currentPos.Item1 + 1, currentPos.Item2);
                    this.Snake.Move(nextPos);
                    break;
                case Direction.Down:
                    nextPos = (currentPos.Item1 - 1, currentPos.Item2);
                    this.Snake.Move(nextPos);
                    break;
                case Direction.Left:
                    nextPos = (currentPos.Item1, currentPos.Item2-1);
                    this.Snake.Move(nextPos);
                    break;
                case Direction.Right:
                    nextPos = (currentPos.Item1, currentPos.Item2+1);
                    this.Snake.Move(nextPos);
                    break;
                default:
                    break;
            }

        }
        private void Render()
        {
            Console.WriteLine($"Map: {this.Height} x {this.Width}");
            this.DrawMap(this.Snake);
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

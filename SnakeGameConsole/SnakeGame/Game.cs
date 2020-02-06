using System;
using System.Collections.Generic;

namespace SnakeGame
{
    internal class Game
    {
        private bool end = false;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public string[,] Map { get; private set; }
        public Snake Snake { get; private set; }
        public Apple Apple { get; private set; }
        

        public Game() : this(20, 30)
        {
        }

        public Game(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Snake = CreateDefaultSnake();
            this.GenerateApple();
            this.Render();
            this.Play();
        }
        private Snake CreateDefaultSnake()
        {
            var defaultSnake = new Snake(new List<(int, int)>() { (3, 5), (3, 4), (3, 3), (3, 2), (3, 1) }, Direction.Right);
            return defaultSnake;
        }

        private void Play()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                while (!isValidKey(key))
                {
                    key = Console.ReadKey(true);
                }

                if (key.Key != ConsoleKey.Escape)
                {
                    var direction = GetDirectionFromKey(key.Key);
                    this.Move(direction);
                    if(!this.end)
                    {
                        Console.Clear();
                        this.Render();
                    }
                    else
                    {
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Press escape key to quit! Press any key to start new game!");
                        key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Escape)
                        {
                            NewGame();
                        }                        
                    }
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Create new game
        /// </summary>
        private void NewGame()
        {
            Console.Clear();
            this.Snake = CreateDefaultSnake();
            this.GenerateApple();
            this.Render();
            this.end = false;
        }

        /// <summary>
        /// Check if the input key is allowed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool isValidKey(ConsoleKeyInfo key)
        {
            var validKeys = new List<ConsoleKey>() { ConsoleKey.Enter, ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleKey.S, ConsoleKey.Escape };
            if (validKeys.Contains(key.Key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get direction from input key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Direction GetDirectionFromKey(ConsoleKey key)
        {
            Direction direction;
            switch (key)
            {
                case ConsoleKey.Enter:
                    direction = this.Snake.Direction;
                    break;
                case ConsoleKey.A:
                    direction = Direction.Left;
                    break;
                case ConsoleKey.D:
                    direction = Direction.Right;
                    break;
                case ConsoleKey.S:
                    direction = Direction.Down;
                    break;
                case ConsoleKey.W:
                    direction = Direction.Up;
                    break;
                default:
                    direction = this.Snake.Direction;
                    break;
            }
            return direction;
        }

        /// <summary>
        /// Move the snake around
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Direction direction)
        {
            (int, int) currentPos = this.Snake.Head;
            
            if(!isPossibleToMove(direction))
            {
                return;
            }

            (int, int) nextPos = currentPos;

            switch (direction)
            {
                case Direction.Up:
                    nextPos = (currentPos.Item1 - 1, currentPos.Item2);
                    break;
                case Direction.Down:
                    nextPos = (currentPos.Item1 + 1, currentPos.Item2);                    
                    break;
                case Direction.Left:
                    nextPos = (currentPos.Item1, currentPos.Item2 - 1);                    
                    break;
                case Direction.Right:
                    nextPos = (currentPos.Item1, currentPos.Item2 + 1);                    
                    break;
                default:
                    break;
            }

            if(!this.Snake.Move(nextPos))
            {
                Console.WriteLine("Game end!!!");
                this.end = true;                
            }
            else
            {
                this.Snake.SetDirection(direction);
            }            
        }
        /// <summary>
        /// Check if next move is possible
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool isPossibleToMove(Direction direction)
        {
            var currentDirection = this.Snake.Direction;

            if (currentDirection == Direction.Left && direction == Direction.Right)
            {
                return false;
            }
            if (currentDirection == Direction.Right && direction == Direction.Left)
            {
                return false;
            }
            if (currentDirection == Direction.Up && direction == Direction.Down)
            {
                return false;
            }
            if (currentDirection == Direction.Down && direction == Direction.Up)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Draw and print the map
        /// </summary>
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
            for (int j = 0; j < this.Width; j++)
            {
                this.Map[0, j] = "*";
                this.Map[this.Height - 1, j] = "*";
            }

            //Vertical walls
            for (int i = 1; i < this.Height - 1; i++)
            {
                this.Map[i, 0] = "*";
                this.Map[i, this.Width - 1] = "*";
            }


            //Field
            for (int i = 1; i < this.Height - 1; i++)
            {
                for (int j = 1; j < this.Width - 1; j++)
                {
                    if (snake.SnakeBody.Contains((i, j)))
                    {
                        this.Map[i, j] = "0";
                    }
                    else
                    {
                        this.Map[i, j] = "";
                    }
                }
            }

            //Apple
            this.Map[this.Apple.X, this.Apple.Y] = "A";

            //Snake head
            this.Map[snake.Head.Item1, snake.Head.Item2] = "X";
        }

        /// <summary>
        /// Check and generate and apple on the map
        /// </summary>
        /// <returns></returns>
        private bool GenerateApple()
        {
            var xMax = this.Height - 2;
            var yMax = this.Width - 2;
            var field = xMax * yMax;

            if (this.Snake.SnakeBody.Count >= field )
            {
                return false;
            }

            var rand = new Random();
            int x;
            int y;
            bool isSnake = true;
            do
            {
                x = rand.Next(1, xMax);
                y = rand.Next(1, yMax);

                isSnake = this.Snake.SnakeBody.Contains((x, y));

            } while (isSnake);

            this.Apple = new Apple(x, y);
            return true;
        }

        public void PrintMap()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Console.Write(this.Map[i, j].PadLeft(1) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Snake
    {
        public List<(int,int)> SnakeBody { get; set; }
        public Direction Direction { get; set; }
        public (int,int) Head { get { return this.SnakeBody[0]; } }
        public Snake(List<(int,int)> initBody, Direction initDirection)
        {
            this.SnakeBody = new List<(int, int)>(initBody);           
            this.Direction = initDirection;
        }

        public void SetDirection(Direction direction)
        {
            this.Direction = direction;
        }

        public void Move((int,int)position)
        {
            this.SnakeBody.Insert(0, position);
            this.SnakeBody.RemoveAt(this.SnakeBody.Count - 1);
        }
    }
    public enum Direction
    {
        Up, Down,Left, Right
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Apple
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Apple(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public override string ToString()
        {
            return String.Format($"[{X}:{Y}]");
        }
        
    }
}

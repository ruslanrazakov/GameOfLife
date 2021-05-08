using System;

namespace GameOfLife.Core
{
    public class Cell
    {
        public bool Alive { get; private set; } 

        public void Kill() => Alive = false;

        public void Renew() => Alive = true;
    }
}

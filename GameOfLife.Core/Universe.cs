using System;
using System.Collections.Generic;

namespace GameOfLife.Core
{
    public class Universe
    {
        public int UniverseSize { get; set; }
        public int MinCellsAround { get; set; }
        public int MaxCellsAround { get; set; }
        public Dictionary<Point, Cell> CurrentGeneration { get; private set; }
        public Dictionary<Point, Cell> NextGeneration { get; private set; }
        
        public Universe()
        {
            CurrentGeneration = new Dictionary<Point, Cell>();
            NextGeneration = new Dictionary<Point, Cell>();
        }

        public void UpdateUniverse()
        {
            foreach(var cellPosition in CurrentGeneration)
            {
                var alive  = CheckCellState(cellPosition.Key, CurrentGeneration);

                if(alive) NextGeneration[cellPosition.Key].Renew();
                else NextGeneration[cellPosition.Key].Kill();

                CurrentGeneration = NextGeneration;
            }
        }

        private bool CheckCellState(Point cellCoord, Dictionary<Point, Cell> currentGeneration)
        {
            int livesCounter = 0;
            var neighbours = new HashSet<Point>();

            for(int i = -1; i <= 1; i++)
            {
                neighbours.Add(new Point(cellCoord.X + i, cellCoord.Y));
                neighbours.Add(new Point(cellCoord.X, cellCoord.Y + i));
                neighbours.Add(new Point(cellCoord.X + i, cellCoord.Y + i));
                neighbours.Add(new Point(cellCoord.X - i, cellCoord.Y + i));
                neighbours.Add(new Point(cellCoord.X + i, cellCoord.Y - i));
            }

            foreach(var neighbour in neighbours)
            {
                if(currentGeneration.ContainsKey(neighbour) && currentGeneration[neighbour].Alive)
                    livesCounter++;
            }

            return livesCounter >= MinCellsAround && livesCounter <= MaxCellsAround;
        }

        public void FillUniverse()
        {

        }
    }
}
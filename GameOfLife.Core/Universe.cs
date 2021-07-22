using System;
using System.Collections.Generic;

namespace GameOfLife.Core
{
    public class Universe
    {
        public int Size { get; set; }
        public int MinCellsAround { get; set; } = 2;
        public int MaxCellsAround { get; set; } = 3;
        public Dictionary<Point, Cell> CurrentGeneration { get; private set; }
        public Dictionary<Point, Cell> NextGeneration { get; private set; }
        
        public Universe()
        {
            CurrentGeneration = new Dictionary<Point, Cell>();
            NextGeneration = new Dictionary<Point, Cell>();
        }

        public void Update()
        {
            CleanNextGeneration();
            //TODO: need refactoring and complete
            foreach(var cellCoord in CurrentGeneration)
            {
                var alive  = CheckCellState(cellCoord.Key);

                if(alive)
                    NextGeneration[new Point((cellCoord.Key.X + Size)%Size, (cellCoord.Key.Y + Size)%Size)].Renew();
                else 
                    NextGeneration[new Point((cellCoord.Key.X + Size)%Size, (cellCoord.Key.Y + Size)%Size)].Kill();

            }
        }

        private void CleanNextGeneration()
        {
            foreach(var cell in NextGeneration)
                NextGeneration[cell.Key].Kill();
        }

        /// <summary>
        /// Checks square around cell and returns true, if cell is alive.
        /// </summary>
        /// <param name="cellCoord"></param>
        /// <param name="currentGeneration"></param>
        /// <returns></returns>
        private bool CheckCellState(Point cellCoord)
        {
            int livesCounter = 0;
            
            var neighbours = new Point []
            {
                new Point(cellCoord.X - 1, cellCoord.Y -1),
                new Point(cellCoord.X, cellCoord.Y - 1),
                new Point(cellCoord.X + 1,  cellCoord.Y - 1),
                new Point(cellCoord.X - 1, cellCoord.Y),
                new Point(cellCoord.X + 1, cellCoord.Y),
                new Point(cellCoord.X - 1, cellCoord.Y + 1),
                new Point(cellCoord.X, cellCoord.Y + 1),
                new Point(cellCoord.X + 1, cellCoord.Y + 1),
            };

            foreach(var neighbour in neighbours)
            {
                if(CurrentGeneration.ContainsKey(neighbour) && CurrentGeneration[neighbour].Alive)
                    livesCounter++;
            }
            
            if((livesCounter == 3 || livesCounter == 2) && CurrentGeneration[new Point((cellCoord.X + Size)%Size, (cellCoord.Y + Size)%Size)].Alive)
                return true;
            else if(livesCounter == 3)
                return true;
            else if(livesCounter < 2 || livesCounter > 3)
                return false;
            else
                return false;
        }

        public void CreateEmpty(int universeSize)
        {
            Size = universeSize;
            for(int column = 0; column < universeSize; column++)
            {
                for(int raw = 0; raw < universeSize; raw++)
                {
                    CurrentGeneration.Add(new Point(raw, column), new Cell());
                    NextGeneration.Add(new Point(raw, column), new Cell());
                }
            }
        }

        public void DrawCell(Point point)
        {
            CurrentGeneration[point].Renew();
        }

        public void RemoveCell(Point point)
        {
            CurrentGeneration[point].Kill();
        }

        public void DrawGlider(Point startCoords)
        {
            if(InBounds(startCoords, figureSize: 3))
            {
                CurrentGeneration[new Point(startCoords.X + 1, startCoords.Y)].Renew();
                CurrentGeneration[new Point(startCoords.X + 2, startCoords.Y + 1)].Renew();
                CurrentGeneration[new Point(startCoords.X, startCoords.Y + 2)].Renew();
                CurrentGeneration[new Point(startCoords.X + 1, startCoords.Y + 2)].Renew();
                CurrentGeneration[new Point(startCoords.X + 2, startCoords.Y + 2)].Renew();
            }
        }

        private bool InBounds(Point startCoords, int figureSize)
        {
            for (int column = startCoords.Y; column < startCoords.Y + figureSize; column++)
            {
                for (int raw = startCoords.X; raw < startCoords.X + figureSize ; raw++)
                {
                    if(!CurrentGeneration.ContainsKey(new Point(raw, column)))
                        return false;
                }
            }
            return true;
        }
    }
}
using AOC.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.PathFinding
{
    internal class FloodFill
    {
        public Grid<int> costAtNodeGrid;
        public Grid<GridConnectionNode> connectionGrid;

        private Grid<int> calculatedCostsGrid; // -1 unknown, 0 target, above 0 is cost
        private Grid<Coord> parentGrid;

        private List<Coord> open;

        private bool wrapEdges;

        public FloodFill(Grid<int> costAtNodeGrid, Grid<GridConnectionNode> connectionGrid, bool wrapEdges)
        {
            this.costAtNodeGrid = costAtNodeGrid;
            this.connectionGrid = connectionGrid;
            this.wrapEdges = wrapEdges;
            calculatedCostsGrid = new Grid<int>();
            for(int i = 0;i<calculatedCostsGrid.nodes.Count();i++)
            {
                calculatedCostsGrid[i] = -1;
            }
            parentGrid = new Grid<Coord>();
            open = new List<Coord>();
        }

        public void AddTargets(List<Coord> targets)
        {
            foreach(var target in targets)
            {
                calculatedCostsGrid[target] = 0;
                open.Add(target);
            }

            Update();
        }

        private void Update()
        {
            while(open.Count > 0)
            {
                var neighbours = GridConnectionNode.ConnectedNeighbourCoords(connectionGrid, open[0], wrapEdges, false);
                foreach(var neighbour in neighbours)
                {
                    var newCost = calculatedCostsGrid[open[0]]+costAtNodeGrid[neighbour];
                    if(newCost<)
                }
                open.RemoveAt(0);
            }
        }
    }
}

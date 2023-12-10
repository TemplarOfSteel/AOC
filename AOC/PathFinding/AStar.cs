using AOC.Generics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AOC.PathFinding
{
    internal class AStarField
    {
        public Grid<int> costAtNodeGrid;
        public Grid<GridConnectionNode> connectionGrid;

        private Grid<int> calulatedCostsGrid;
        private Grid<Coord> parentGrid;

        public AStarField(Grid<int> valueAtPointGrid, Grid<GridConnectionNode> connectionGrid)
        {
            this.costAtNodeGrid = valueAtPointGrid;
            this.connectionGrid = connectionGrid;
            calulatedCostsGrid =  new Grid<int>();
            parentGrid = new Grid<Coord>();
        }

        private int EstimateFromStart(Coord current, Coord target, bool wrapEdges, int maxChangePerNode) //estimated cost up to this point
        {
            return RealFromStart(current) + EstimateBetweenTwo(current, target, wrapEdges, maxChangePerNode);
        }

        private int RealFromStart(Coord current) //real cost up to this point
        {
            return calulatedCostsGrid[current];
        }

        private int EstimateBetweenTwo(Coord current, Coord target, bool wrapEdges, int maxChangePerNode) //estimated cost
        {
            if(wrapEdges)
            {
                var diff = current - target;
                diff.col = Math.Min(Math.Abs(diff.col), Math.Abs(costAtNodeGrid.columns - diff.col));
                diff.row = Math.Min(Math.Abs(diff.row), Math.Abs(costAtNodeGrid.columns - diff.row));
                return (diff.col + diff.row) * maxChangePerNode;
            }
            else
            {
                var diff = current - target;
                return (Math.Abs(diff.col) + Math.Abs(diff.row)) * maxChangePerNode;
            }
        }

        private int RealAtPoint(Coord node) //real value
        {
            return costAtNodeGrid[node];
        }

        public List<Coord> GetClosestPath(Coord start, Coord target, bool wrapEdges)
        {
            var connectedNodes = GridConnectionNode.ConnectedCoords(connectionGrid, start, wrapEdges, false);
            if(!connectedNodes.Contains(target))
            {
                throw new Exception("impossible to reach end");
            }

            List<Coord> open = new List<Coord>();
            List<Coord> closed = new List<Coord>();

            open.Add(start);

            int maxValue = 0;
            foreach(var coord in connectedNodes)
            {
                maxValue = Math.Max(maxValue, costAtNodeGrid[coord]);
            }

            while(open.Count > 0)
            {
                open.OrderBy(n => EstimateFromStart(n, target, wrapEdges, maxValue));

                if (open[0] == target) { break; }

                foreach(var nCoord in GridConnectionNode.ConnectedNeighbourCoords(connectionGrid, open[0], wrapEdges, false))
                {
                    var newSuccessorCost = RealFromStart(open[0]) + RealAtPoint(nCoord);
                    if(open.Contains(nCoord))
                    {
                        if(RealFromStart(nCoord) <= newSuccessorCost)
                        {
                            break;
                        }
                    }
                    else if(closed.Contains(nCoord))
                    {
                        if (RealFromStart(nCoord) <= newSuccessorCost)
                        {
                            break;
                        }

                        closed.Remove(nCoord);
                        open.Add(nCoord);
                    }
                    else
                    {
                        open.Add(nCoord);
                    }

                    calulatedCostsGrid[nCoord] = newSuccessorCost;
                    parentGrid[nCoord] = open[0];
                }
                closed.Add(open[0]);
                open.RemoveAt(0);
            }

            var ret = new List<Coord>() { target};
            while(ret.Last() != target)
            {
                ret.Add(parentGrid[ret.Last()]);
            }

            return null;
        }
    }
}

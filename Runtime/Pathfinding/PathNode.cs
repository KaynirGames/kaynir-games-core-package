using KaynirGames.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Узел для поиска пути.
    /// </summary>
    public class PathNode : IHeapNode<PathNode>
    {
        private const int STRAIGHT_STEP_COST = 10;
        private const int DIAGONAL_STEP_COST = 14;

        public Vector2Int GridPosition { get; private set; }
        public Vector2 WorldPosition { get; private set; }

        public bool IsObstacle { get; private set; }

        public int GCost { get; private set; }
        public int HCost { get; private set; }
        public int FCost => GCost + HCost;

        public PathNode FromNode { get; private set; }
        public PathNode[] Neighbours { get; private set; }

        public int HeapIndex { get; set; }

        public PathNode(int gridX, int gridY, Vector2 worldPosition, bool isObstacle)
        {
            GridPosition = new Vector2Int(gridX, gridY);
            WorldPosition = worldPosition;
            IsObstacle = isObstacle;
            GCost = 0;
            HCost = 0;
            FromNode = null;
        }

        public void UpdateNode(int gCost, int hCost, PathNode fromNode)
        {
            GCost = gCost;
            HCost = hCost;
            FromNode = fromNode;
        }

        public int GetDistanceCost(PathNode otherNode)
        {
            int distanceX = Mathf.Abs(GridPosition.x - otherNode.GridPosition.x);
            int distanceY = Mathf.Abs(GridPosition.y - otherNode.GridPosition.y);
            int difference = Mathf.Abs(distanceX - distanceY);

            return Mathf.Min(distanceX, distanceY) * DIAGONAL_STEP_COST + difference * STRAIGHT_STEP_COST;
        }

        public void SetNeighbours(List<PathNode> neighbours) => Neighbours = neighbours.ToArray();

        public int CompareTo(PathNode otherNode)
        {
            return FCost != otherNode.FCost
                ? FCost - otherNode.FCost
                : HCost - otherNode.HCost;
        }
    }
}

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
        private const int STRAIGHT_STEP_COST = 10; // Стоимость прямого шага.
        private const int DIAGONAL_STEP_COST = 14; // Стоимость шага по диагонали.
        /// <summary>
        /// Позиция узла в сетке.
        /// </summary>
        public Vector2Int GridPosition { get; private set; }
        /// <summary>
        /// Мировая позиция узла.
        /// </summary>
        public Vector2 WorldPosition { get; private set; }
        /// <summary>
        /// Узел является препятствием на пути?
        /// </summary>
        public bool IsObstacle { get; private set; }
        /// <summary>
        /// Расстояние от узла до начальной точки пути.
        /// </summary>
        public int GCost { get; private set; }
        /// <summary>
        /// Расстояние от узла до конечной точки пути.
        /// </summary>
        public int HCost { get; private set; }
        /// <summary>
        /// Суммарная стоимость пути.
        /// </summary>
        public int FCost => GCost + HCost;
        /// <summary>
        /// Узел, из которого осуществлен переход.
        /// </summary>
        public PathNode FromNode { get; private set; }
        /// <summary>
        /// Соседние узлы.
        /// </summary>
        public PathNode[] Neighbours { get; private set; }
        /// <summary>
        /// Индекс узла в куче (реализация IHeapNode).
        /// </summary>
        public int HeapIndex { get; set; }
        /// <summary>
        /// Новый узел для поиска пути.
        /// </summary>
        public PathNode(int gridX, int gridY, Vector2 worldPosition, bool isObstacle)
        {
            GridPosition = new Vector2Int(gridX, gridY);
            WorldPosition = worldPosition;
            IsObstacle = isObstacle;
            GCost = 0;
            HCost = 0;
            FromNode = null;
        }
        /// <summary>
        /// Обновить узел.
        /// </summary>
        public void UpdateNode(int gCost, int hCost, PathNode fromNode)
        {
            GCost = gCost;
            HCost = hCost;
            FromNode = fromNode;
        }
        /// <summary>
        /// Найти стоимость пути до другого узла.
        /// </summary>
        public int GetDistanceCost(PathNode otherNode)
        {
            int distanceX = Mathf.Abs(GridPosition.x - otherNode.GridPosition.x);
            int distanceY = Mathf.Abs(GridPosition.y - otherNode.GridPosition.y);
            int difference = Mathf.Abs(distanceX - distanceY);

            return Mathf.Min(distanceX, distanceY) * DIAGONAL_STEP_COST + difference * STRAIGHT_STEP_COST;
        }
        /// <summary>
        /// Запомнить соседей узла.
        /// </summary>
        public void SetNeighbours(List<PathNode> neighbours) => Neighbours = neighbours.ToArray();
        /// <summary>
        /// Сравнить с другим узлом (реализация IHeapNode).
        /// </summary>
        public int CompareTo(PathNode otherNode)
        {
            return FCost != otherNode.FCost
                ? FCost - otherNode.FCost
                : HCost - otherNode.HCost;
        }
    }
}

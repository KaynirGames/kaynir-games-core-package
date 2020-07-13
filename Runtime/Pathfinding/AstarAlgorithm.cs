﻿using KaynirGames.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Алгоритм поиска маршрута с наименьшей стоимостью.
    /// </summary>
    public class AstarAlgorithm
    {
        private Grid<PathNode> _grid; // Сетка узлов для поиска пути.
        private MinBinaryHeap<PathNode> _openSet; // Набор узлов для проверки. 
        private HashSet<PathNode> _closedSet; // Набор проверенных узлов.

        public AstarAlgorithm(Grid<PathNode> grid)
        {
            _grid = grid;
        }
        /// <summary>
        /// Рассчитать оптимальный маршрут.
        /// </summary>
        public Path CalculatePath(Vector2 startPoint, Vector2 endPoint)
        {
            PathNode startNode = _grid.GetValue(startPoint);
            PathNode endNode = _grid.GetValue(endPoint);
            bool pathSuccess = false;
            PathNode[] pathNodes = null;
            
            if (endNode.IsObstacle)
            {
                // Пробуем дойти до ближайшего соседа конечного узла.
                endNode = TryOptimalNeighbour(startNode, endNode);

                if (endNode == null) return new Path(null, false);
            }

            _openSet = new MinBinaryHeap<PathNode>();
            _closedSet = new HashSet<PathNode>();

            _openSet.Add(startNode);

            while (_openSet.HeapSize > 0)
            {
                PathNode currentNode = _openSet.RemoveFirst();
                _closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    pathNodes = RetracePath(startNode, endNode);
                    if (pathNodes.Length > 0) pathSuccess = true;
                    break;
                }

                if (currentNode.Neighbours == null) currentNode.SetNeighbours(GetNeighbours(currentNode));

                foreach (PathNode neighbour in currentNode.Neighbours)
                {
                    if (_closedSet.Contains(neighbour)) continue;
                    CheckNeighbour(currentNode, neighbour, endNode);
                }
            }

            return new Path(pathNodes, pathSuccess);
        }
        /// <summary>
        /// Попробовать найти оптимального соседа конечного узла.
        /// </summary>
        private PathNode TryOptimalNeighbour(PathNode startNode, PathNode endNode)
        {
            endNode.SetNeighbours(GetNeighbours(endNode));

            if (endNode.Neighbours.Length == 0) return null;

            PathNode optimalNode = endNode.Neighbours[0];
            int optimalDistance = optimalNode.GetDistanceCost(startNode);

            foreach (PathNode neighbour in endNode.Neighbours)
            {
                int currentDistance = neighbour.GetDistanceCost(startNode);
                if (currentDistance < optimalDistance)
                {
                    optimalDistance = currentDistance;
                    optimalNode = neighbour;
                }
            }
            return optimalNode;
        }
        /// <summary>
        /// Проверить стоимость соседа текущего узла.
        /// </summary>
        private void CheckNeighbour(PathNode currentNode, PathNode neighbour, PathNode endNode)
        {
            int newDistanceCost = currentNode.GCost + currentNode.GetDistanceCost(neighbour);

            if (newDistanceCost < neighbour.GCost || !_openSet.Contains(neighbour))
            {
                neighbour.UpdateNode(newDistanceCost, neighbour.GetDistanceCost(endNode), currentNode);

                if (!_openSet.Contains(neighbour))
                {
                    _openSet.Add(neighbour);
                }
                else
                {
                    _openSet.Heapify(neighbour); // Так как обновилась стоимость узла.
                }
            }
        }
        /// <summary>
        /// Найти соседей узла.
        /// </summary>
        private List<PathNode> GetNeighbours(PathNode pathNode)
        {
            List<PathNode> neighbours = new List<PathNode>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Пропускаем центр блока 3х3 (позиция самого узла).
                    if (x == 0 && y == 0)
                        continue;
                    // Вычисляем индексы соседнего узла в сетке. 
                    int posX = pathNode.GridPosition.x + x;
                    int posY = pathNode.GridPosition.y + y;
                    // Проверяем, чтобы индексы не выходили за границы массива.
                    if (posX >= 0 && posX < _grid.GetLength(0) && posY >= 0 && posY < _grid.GetLength(1))
                    {
                        PathNode neighbour = _grid.GetValue(posX, posY);
                        if (!neighbour.IsObstacle)
                        {
                            neighbours.Add(neighbour);
                        }
                    }
                }
            }

            return neighbours;
        }
        /// <summary>
        /// Записать пройденный маршрут.
        /// </summary>
        private PathNode[] RetracePath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            PathNode currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.FromNode;
            }

            path.Reverse();

            return path.ToArray();
        }
    }
}

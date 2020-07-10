﻿using KaynirGames.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Искатель оптимального пути.
    /// </summary>
    public class Pathfinder : MonoBehaviour
    {
        /// <summary>
        /// Событие при смене действующего искателя пути.
        /// </summary>
        public static event Action<Pathfinder> OnActivePathfinderChange = delegate { };

        public static Pathfinder Instance { get; private set; }

        [SerializeField] private Vector2Int _gridSize = Vector2Int.one; // Величина сетки.
        [SerializeField] private float _nodeSize = 1f; // Величина узла.
        [SerializeField] private LayerMask _obstacleMask = new LayerMask(); // Для определения узлов, являющихся препятствием.
        [SerializeField] private bool _displayGrid = false; // Отображать сетку узлов?

        private Grid<PathNode> _grid; // Сетка узлов для поиска пути.
        private MinBinaryHeap<PathNode> _openSet; // Набор узлов для проверки. 
        private HashSet<PathNode> _closedSet; // Набор проверенных узлов.
        private PathNode _startNode; // Начальный узел пути. 
        private PathNode _endNode; // Конечный узел пути.

        /// <summary>
        /// Создать сетку для поиска пути.
        /// </summary>
        public void CreateGrid()
        {
            _grid = new Grid<PathNode>(_gridSize, _nodeSize, transform.position);

            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    Vector2 worldPosition = _grid.GetWorldPosition(x, y);
                    bool isObstacle = Physics2D.OverlapCircle(worldPosition, _nodeSize * .5f, _obstacleMask);
                    PathNode newNode = new PathNode(x, y, worldPosition, isObstacle);
                    _grid.SetValue(x, y, newNode);
                }
            }
        }
        /// <summary>
        /// Найти оптимальный маршрут.
        /// </summary>
        public Path FindPath(Vector2 startPoint, Vector2 endPoint)
        {
            if (_grid == null) CreateGrid();

            _startNode = _grid.GetValue(startPoint);
            _endNode = _grid.GetValue(endPoint);
            bool pathSuccess = false;
            PathNode[] pathNodes = null;

            if (_endNode.IsObstacle)
            {
                // Пробуем дойти до ближайшего соседа конечного узла.
                _endNode = TryOptimalNeighbour(_startNode, _endNode);
                if (_endNode == null)
                {
                    return new Path(null, false);
                }
            }

            _openSet = new MinBinaryHeap<PathNode>();
            _closedSet = new HashSet<PathNode>();

            _openSet.Add(_startNode);

            while (_openSet.HeapSize > 0)
            {
                PathNode currentNode = _openSet.RemoveFirst();
                _closedSet.Add(currentNode);

                if (currentNode == _endNode)
                {
                    pathNodes = RetracePath(_startNode, _endNode);
                    if (pathNodes.Length > 0) pathSuccess = true;
                    break;
                }

                if (currentNode.Neighbours == null) currentNode.SetNeighbours(GetNeighbours(currentNode));

                foreach (PathNode neighbour in currentNode.Neighbours)
                {
                    if (_closedSet.Contains(neighbour)) continue;
                    CheckNeighbour(currentNode, neighbour);
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
        private void CheckNeighbour(PathNode currentNode, PathNode neighbour)
        {
            int newDistanceCost = currentNode.GCost + currentNode.GetDistanceCost(neighbour);

            if (newDistanceCost < neighbour.GCost || !_openSet.Contains(neighbour))
            {
                neighbour.UpdateNode(newDistanceCost, neighbour.GetDistanceCost(_endNode), currentNode);

                if (!_openSet.Contains(neighbour))
                {
                    _openSet.Add(neighbour);
                }
                else
                {
                    _openSet.Heapify(neighbour);
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

        private void OnEnable()
        {
            // Уведомляем о включении действующего искателя.
            if (Instance == null) Instance = this;
        }

        private void OnDisable()
        {
            // Уведомляем об отключении действующего искателя.
            // Это разрешит seeker-объекту установить нового искателя.
            Instance = null;
        }

        private void OnDrawGizmos()
        {
            if (_displayGrid)
            {
                if (_grid == null) return;

                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _grid.GetLength(1); y++)
                    {
                        PathNode pathNode = _grid.GetValue(x, y);
                        Gizmos.color = pathNode.IsObstacle ? Color.red : Color.cyan;
                        Gizmos.DrawWireCube(pathNode.WorldPosition, new Vector2(_nodeSize - .1f, _nodeSize - .1f));
                    }
                }
            }
        }
    }
}

using KaynirGames.Collections;
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
        public static Pathfinder Instance { get; private set; }

        [SerializeField] private Vector2Int _gridSize = Vector2Int.one; // Величина сетки.
        [SerializeField] private float _nodeSize = 1f; // Величина узла.
        [SerializeField] private LayerMask _obstacleMask = new LayerMask(); // Для определения узлов, являющихся препятствием.
        [SerializeField] private bool _displayGrid = false; // Отображать сетку узлов?

        private Grid<PathNode> _grid; // Сетка узлов для поиска маршрута.
        private AstarAlgorithm _astarAlgorithm; // Алгоритм поиска маршрута.
        private List<Vector2> _allWorldPoints = new List<Vector2>(); // Доступные мировые точки в сетке.
        private List<Vector2> _freeWorldPoints = new List<Vector2>(); // Мировые точки без препятствий в сетке.
 
        /// <summary>
        /// Создать сетку для поиска маршрута.
        /// </summary>
        public void Initialize()
        {
            CreateGrid();
            _astarAlgorithm = new AstarAlgorithm(_grid);
        }
        /// <summary>
        /// Найти оптимальный маршрут.
        /// </summary>
        public Path FindPath(Vector2 startPoint, Vector2 endPoint)
        {
            if (_grid == null) Initialize();

            return _astarAlgorithm.CalculatePath(startPoint, endPoint);
        }
        /// <summary>
        /// Получить мировые точки, используемые сеткой поиска маршрута.
        /// </summary>
        public Vector2[] GetGridWorldPoints(bool includeObstacles)
        {
            return includeObstacles
                ? _allWorldPoints.ToArray() 
                : _freeWorldPoints.ToArray();
        }
        /// <summary>
        /// Создать сетку для поиска маршрута.
        /// </summary>
        private void CreateGrid()
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

                    _allWorldPoints.Add(worldPosition);
                    if (!isObstacle) _freeWorldPoints.Add(worldPosition);
                }
            }
        }

        private void OnEnable()
        {
            if (Instance == null) Instance = this;
        }

        private void OnDisable()
        {
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

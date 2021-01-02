using KaynirGames.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Искатель оптимального пути.
    /// </summary>
    public class Pathfinder : MonoBehaviour
    {
        private static Pathfinder _current;

        [SerializeField] private Vector2Int _gridSize = Vector2Int.one;
        [SerializeField] private float _nodeSize = 1f;
        [SerializeField] private LayerMask _obstacleMask = new LayerMask();
        [SerializeField] private bool _displayGrid = false;

        private Grid<PathNode> _grid;
        private AstarAlgorithm _astarAlgorithm;

        private List<Vector2> _allWorldPoints = new List<Vector2>();
        private List<Vector2> _freeWorldPoints = new List<Vector2>();

        public void Initialize()
        {
            CreateGrid();

            _astarAlgorithm = new AstarAlgorithm(_grid);
        }

        public static Path FindPath(Vector2 startPoint, Vector2 endPoint)
        {
            if (_current._grid == null)
            {
                _current.Initialize();
            }

            return _current._astarAlgorithm.CalculatePath(startPoint, endPoint);
        }

        public static Vector2[] GetGridWorldPoints(bool includeObstacles)
        {
            return includeObstacles
                ? _current._allWorldPoints.ToArray()
                : _current._freeWorldPoints.ToArray();
        }

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
            if (_current == null)
            {
                _current = this;
            }
        }

        private void OnDisable()
        {
            _current = null;
        }

        private void OnValidate()
        {
            CreateGrid();
        }

        private void OnDrawGizmos()
        {
            if (_displayGrid && _grid != null)
            {
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

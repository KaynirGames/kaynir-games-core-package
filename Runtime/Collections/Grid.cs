using UnityEngine;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Сетка элементов.
    /// </summary>
    public class Grid<TGridObject>
    {
        private Vector2Int _gridSize;
        private float _cellSize;
        private Vector2 _originPosition;
        private TGridObject[,] _gridArray;
        private int _cellCountX;
        private int _cellCountY;

        public Grid(Vector2Int gridSize, float cellSize, Vector2 parentPosition)
        {
            _gridSize = gridSize;
            _cellSize = cellSize;
            _cellCountX = (int)(_gridSize.x / _cellSize);
            _cellCountY = (int)(_gridSize.y / _cellSize);
            _originPosition = parentPosition - new Vector2(_gridSize.x, _gridSize.y) * .5f;
            _gridArray = new TGridObject[_cellCountX, _cellCountY];
        }

        public int GetLength(int dimention)
        {
            return _gridArray == null ? 0 : _gridArray.GetLength(dimention);
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return _originPosition + new Vector2(x + .5f, y + .5f) * _cellSize;
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            int x = (int)((worldPosition.x - _originPosition.x) / _cellSize);
            int y = (int)((worldPosition.y - _originPosition.y) / _cellSize);

            Vector2Int gridPos = new Vector2Int()
            {
                x = Mathf.Clamp(x, 0, _cellCountX - 1),
                y = Mathf.Clamp(y, 0, _cellCountY - 1)
            };

            return gridPos;
        }

        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && x < _cellCountX && y >= 0 && y < _cellCountY)
            {
                _gridArray[x, y] = value;
            }
        }

        public void SetValue(Vector2 worldPosition, TGridObject value)
        {
            Vector2Int gridPos = GetGridPosition(worldPosition);
            SetValue(gridPos.x, gridPos.y, value);
        }

        public TGridObject GetValue(int x, int y)
        {
            if (x >= 0 && x < _cellCountX && y >= 0 && y < _cellCountY)
            {
                return _gridArray[x, y];
            }
            else return default;
        }

        public TGridObject GetValue(Vector2 worldPosition)
        {
            Vector2Int gridPos = GetGridPosition(worldPosition);
            return GetValue(gridPos.x, gridPos.y);
        }
    }
}

using UnityEngine;

namespace KaynirGames.Collections
{
    /// <summary>
    /// Сетка элементов.
    /// </summary>
    public class Grid<TGridObject>
    {
        private Vector2Int _gridSize; // Размер сетки.
        private float _cellSize; // Размер ячейки.
        private Vector2 _originPosition; // Исходная позиция, откуда начинается построение сетки.
        private TGridObject[,] _gridArray; // Массив элементов сетки.
        private int _cellCountX; // Число ячеек по оси X.
        private int _cellCountY; // Число ячеек по оси Y.
        /// <summary>
        /// Новая сетка элементов.
        /// </summary>
        /// <param name="gridSize">Величина сетки.</param>
        /// <param name="cellSize">Величина ячейки.</param>
        /// <param name="parentPosition">Позиция объекта, для которого строится сетка.</param>
        public Grid(Vector2Int gridSize, float cellSize, Vector2 parentPosition)
        {
            _gridSize = gridSize;
            _cellSize = cellSize;
            _cellCountX = (int)(_gridSize.x / _cellSize);
            _cellCountY = (int)(_gridSize.y / _cellSize);
            _originPosition = parentPosition - new Vector2(_gridSize.x, _gridSize.y) * .5f;
            _gridArray = new TGridObject[_cellCountX, _cellCountY];
        }
        /// <summary>
        /// Получить длину массива сетки.
        /// </summary>
        public int GetLength(int dimention)
        {
            return _gridArray == null ? 0 : _gridArray.GetLength(dimention);
        }
        /// <summary>
        /// Получить мировую позицию элемента по индексу сетки.
        /// </summary>
        public Vector2 GetWorldPosition(int x, int y)
        {
            return _originPosition + new Vector2(x + .5f, y + .5f) * _cellSize;
        }
        /// <summary>
        /// Получить индекс элемента сетки по мировой позиции.
        /// </summary>
        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            // Находим индекс подходящего элемента сетки.
            int x = (int)((worldPosition.x - _originPosition.x) / _cellSize);
            int y = (int)((worldPosition.y - _originPosition.y) / _cellSize);
            // Преобразуем, чтобы индекс не выходил за границы массива.
            Vector2Int gridPos = new Vector2Int()
            {
                x = Mathf.Clamp(x, 0, _cellCountX - 1),
                y = Mathf.Clamp(y, 0, _cellCountY - 1)
            };
            return gridPos;
        }
        /// <summary>
        /// Выставить значение элемента сетки по индексу.
        /// </summary>
        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && x < _cellCountX && y >= 0 && y < _cellCountY)
            {
                _gridArray[x, y] = value;
            }
        }
        /// <summary>
        /// Выставить значение элемента сетки по мировой позиции.
        /// </summary>
        public void SetValue(Vector2 worldPosition, TGridObject value)
        {
            Vector2Int gridPos = GetGridPosition(worldPosition);
            SetValue(gridPos.x, gridPos.y, value);
        }
        /// <summary>
        /// Получить значение элемента сетки по индексу.
        /// </summary>
        public TGridObject GetValue(int x, int y)
        {
            if (x >= 0 && x < _cellCountX && y >= 0 && y < _cellCountY)
            {
                return _gridArray[x, y];
            }
            else return default;
        }
        /// <summary>
        /// Получить значение элемента сетки по мировой позиции.
        /// </summary>
        public TGridObject GetValue(Vector2 worldPosition)
        {
            Vector2Int gridPos = GetGridPosition(worldPosition);
            return GetValue(gridPos.x, gridPos.y);
        }
    }
}

using System.Collections;
using UnityEngine;

namespace KaynirGames.Pathfinding
{
    /// <summary>
    /// Объект, нуждающийся в оптимальном пути.
    /// </summary>
    public class Seeker : MonoBehaviour
    {
        [SerializeField] private float _nextWaypointDistance = 1f; // Расстояние для перехода к следующей точке маршрута.
        [SerializeField] private float _followSpeed = 2f; // Скорость движения по маршруту.
        [SerializeField] private bool _displayPath = false; // Отображать маршрут при движении?

        /// <summary>
        /// Запрос на поиск маршрута завершился?
        /// </summary>
        public bool IsRequestDone { get; private set; }
        /// <summary>
        /// Движение по маршруту завершилось?
        /// </summary>
        public bool IsFollowingDone { get; private set; }

        private Pathfinder _activePathfinder; // Действующий искатель пути, осуществляющий вычисления.
        private Vector2[] _currentPath; // Текущий маршрут.
        private int _currentPointIndex; // Текущий индекс точки маршрута.
        private Coroutine _lastFollowRoutine; // Последняя запущенная корутина движения по маршруту.

        private void Awake()
        {
            Pathfinder.OnActivePathfinderChange += SetActivePathfinder;
            IsRequestDone = true;
            IsFollowingDone = true;
        }
        /// <summary>
        /// Запросить оптимальный маршрут между точками.
        /// </summary>
        public void RequestPath(Vector2 startPoint, Vector2 endPoint)
        {
            IsRequestDone = false;
            _activePathfinder.FindPath(startPoint, endPoint, ReceivePath);
        }
        /// <summary>
        /// Установить скорость движения по маршруту.
        /// </summary>
        public void SetFollowSpeed(float speed) => _followSpeed = speed;
        /// <summary>
        /// Установить действующего искателя пути.
        /// </summary>
        private void SetActivePathfinder(Pathfinder pathfinder)
        {
            // Одновременно может быть активен только один искатель пути.
            // Устанавливаем искателя, если его не было изначально, либо отключился предыдущий.
            if (_activePathfinder == null || pathfinder == null)
            {
                _activePathfinder = pathfinder;
            }
        }
        /// <summary>
        /// Получить оптимальный маршрут.
        /// </summary>
        private void ReceivePath(Vector2[] path, bool pathSuccess)
        {
            IsRequestDone = true;
            if (pathSuccess)
            {
                _currentPath = path;
                if (_lastFollowRoutine != null) StopCoroutine(_lastFollowRoutine);
                _lastFollowRoutine = StartCoroutine(FollowPath());
            }
            else Debug.Log("Путь не найден.");
        }
        /// <summary>
        /// Двигаться по текущему маршруту.
        /// </summary>
        private IEnumerator FollowPath()
        {
            _currentPointIndex = 0;
            IsFollowingDone = false;

            while (_currentPointIndex < _currentPath.Length)
            {
                Vector2 currentWaypoint = _currentPath[_currentPointIndex];
                float distance = Vector2.Distance(transform.position, currentWaypoint);

                if (distance < _nextWaypointDistance)
                {
                    _currentPointIndex++;
                }
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, _followSpeed * Time.deltaTime);
                yield return null;
            }
            IsFollowingDone = true;
        }

        private void OnDrawGizmos()
        {
            if (_displayPath)
            {
                if (_currentPath != null)
                {
                    for (int i = _currentPointIndex; i < _currentPath.Length; i++)
                    {
                        Gizmos.color = Color.green;
                        if (i == _currentPointIndex)
                        {
                            Gizmos.DrawLine(transform.position, _currentPath[i]);
                        }
                        else
                        {
                            Gizmos.DrawLine(_currentPath[i - 1], _currentPath[i]);
                        }
                    }
                }
            }
        }
    }
}

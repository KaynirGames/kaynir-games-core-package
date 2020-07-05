using System;
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
        [SerializeField] private bool _useSimplePath = true; // Использовать упрощенный маршрут?
        [SerializeField] private bool _displayPath = false; // Отображать маршрут при движении?

        /// <summary>
        /// Запрос на поиск маршрута завершился?
        /// </summary>
        public bool IsRequestDone { get; private set; }
        /// <summary>
        /// Движение по маршруту завершилось?
        /// </summary>
        public bool IsFollowingDone { get; private set; }
        /// <summary>
        /// Объект может двигаться?
        /// </summary>
        public bool CanMove { get; set; }

        private Pathfinder _activePathfinder; // Действующий искатель пути, осуществляющий вычисления.
        private Vector2[] _currentWaypoints; // Текущие точки маршрута.
        private int _currentPointIndex; // Текущий индекс точки маршрута.
        private Coroutine _lastFollowRoutine; // Последняя запущенная корутина движения по маршруту.
        private bool _handlePathMovement; // Управлять перемещением по маршруту?

        private void Awake()
        {
            Pathfinder.OnActivePathfinderChange += SetActivePathfinder;
            IsRequestDone = true;
            IsFollowingDone = true;
            CanMove = true;
        }
        /// <summary>
        /// Запросить оптимальный маршрут между точками (сикер управляет движением по маршруту).
        /// </summary>
        public void RequestPath(Vector2 startPoint, Vector2 endPoint)
        {
            IsRequestDone = false;
            _handlePathMovement = true;
            _activePathfinder.FindPath(startPoint, endPoint, CompleteRequest);
        }
        /// <summary>
        /// Запросить оптимальный маршрут между точками (сикер не управляет движением по маршруту).
        /// </summary>
        public void RequestPath(Vector2 startPoint, Vector2 endPoint, Action<Path> onPathReceive)
        {
            IsRequestDone = false;
            _handlePathMovement = false;
            _activePathfinder.FindPath(startPoint, endPoint, onPathReceive);
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
        /// Завершить запрос на получение пути.
        /// </summary>
        private void CompleteRequest(Path path)
        {
            IsRequestDone = true;

            if (!_handlePathMovement) return;

            if (path.Exist)
            {
                _currentWaypoints = _useSimplePath ? path.Simplify() : path.Waypoints;
                if (_lastFollowRoutine != null) StopCoroutine(_lastFollowRoutine);
                _lastFollowRoutine = StartCoroutine(FollowPath());
            }
        }
        /// <summary>
        /// Двигаться по текущему маршруту.
        /// </summary>
        private IEnumerator FollowPath()
        {
            _currentPointIndex = 0;
            IsFollowingDone = false;

            while (_currentPointIndex < _currentWaypoints.Length)
            {
                if (CanMove)
                {
                    Vector2 currentWaypoint = _currentWaypoints[_currentPointIndex];
                    float distance = Vector2.Distance(transform.position, currentWaypoint);

                    if (distance < _nextWaypointDistance)
                    {
                        _currentPointIndex++;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, _followSpeed * Time.deltaTime);
                }
                yield return null;
            }
            IsFollowingDone = true;
        }

        private void OnDrawGizmos()
        {
            if (_displayPath)
            {
                if (_currentWaypoints != null)
                {
                    for (int i = _currentPointIndex; i < _currentWaypoints.Length; i++)
                    {
                        Gizmos.color = Color.green;
                        if (i == _currentPointIndex)
                        {
                            Gizmos.DrawLine(transform.position, _currentWaypoints[i]);
                        }
                        else
                        {
                            Gizmos.DrawLine(_currentWaypoints[i - 1], _currentWaypoints[i]);
                        }
                    }
                }
            }
        }
    }
}

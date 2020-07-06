﻿using KaynirGames.Pathfinding;
using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Перемещение с использованием алгоритма поиска маршрута.
    /// </summary>
    [RequireComponent(typeof(Seeker))]
    public class PathfindingMovement : BaseMovement
    {
        [SerializeField] private CharacterMoveBase _characterMove = null; // Механика перемещения персонажа.
        [SerializeField] private float _waypointReachedDistance = 0.2f; // Дистанция, при которой точка считается достигнутой.
        [SerializeField] private bool _useSimplePath = true; // Использовать упрощенный маршрут?
        [SerializeField] private bool _displayPath = false; // Отображать найденный маршрут?

        private Seeker _seeker; // Текущий сикер.     
        private Vector2[] _waypoints = new Vector2[0]; // Точки маршрута.
        private int _waypointIndex = -1; // Индекс текущей точки маршрута.

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
        }

        private void Update()
        {
            if (_characterMove == null) return;

            HandleMovement();
        }
        /// <summary>
        /// Задать позицию для перемещения.
        /// </summary>
        public override void SetMovementPosition(Vector3 movePosition)
        {
            _waypoints = _useSimplePath
                ? _seeker.GetSimplePath(transform.position, movePosition)
                : _seeker.GetFullPath(transform.position, movePosition);

            if (_waypoints.Length > 0) _waypointIndex = 0;
            else _waypointIndex = -1;
        }
        /// <summary>
        /// Осуществить перемещение.
        /// </summary>
        protected override void HandleMovement()
        {
            if (_waypointIndex != -1)
            {
                // Двигаться к следующей позиции.
                Vector3 nextPosition = _waypoints[_waypointIndex];
                Vector3 moveDirection = (nextPosition - transform.position).normalized;

                _characterMove.SetMoveDirection(moveDirection);

                if (Vector2.Distance(transform.position, nextPosition) <= _waypointReachedDistance)
                {
                    _waypointIndex++;

                    if (_waypointIndex >= _waypoints.Length)
                    {
                        _waypointIndex = -1; // Конец пути.
                    }
                }
            }
            else
            {
                _characterMove.SetMoveDirection(Vector3.zero);
            }
        }

        private void OnDrawGizmos()
        {
            if (_displayPath && _waypointIndex != -1)
            {
                for (int i = _waypointIndex; i < _waypoints.Length; i++)
                {
                    Gizmos.color = Color.green;
                    if (i == _waypointIndex)
                    {
                        Gizmos.DrawLine(transform.position, _waypoints[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(_waypoints[i - 1], _waypoints[i]);
                    }
                }
            }
        }
    }
}

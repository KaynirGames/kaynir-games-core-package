using KaynirGames.Pathfinding;
using UnityEngine;

namespace KaynirGames.Movement
{
    [RequireComponent(typeof(Seeker))]
    public class MovePositionPathfinding : MovePositionBase
    {
        [SerializeField] private bool _useSimplePath = true;
        [SerializeField] private bool _displayPath = false;

        private Seeker _seeker;   
        private Vector2[] _waypoints = new Vector2[0];
        private int _waypointIndex = -1;

        protected override void Awake()
        {
            base.Awake();

            _seeker = GetComponent<Seeker>();
        }

        private void Update()
        {
            MovePosition();
        }

        public override void SetPosition(Vector3 movePosition)
        {
            _waypoints = _useSimplePath
                ? _seeker.GetSimplePath(transform.position, movePosition)
                : _seeker.GetFullPath(transform.position, movePosition);

            if (_waypoints.Length > 0)
            {
                _waypointIndex = 0;
                IsMoving = true;
            }
            else
            {
                _waypointIndex = -1;
                IsMoving = false;
            }
        }

        public override void StopMovement()
        {
            _waypointIndex = -1;
            _moveBase.SetMoveDirection(Vector3.zero);
            IsMoving = false;
        }

        protected override void MovePosition()
        {
            if (_waypointIndex != -1)
            {
                Vector3 nextPosition = _waypoints[_waypointIndex];
                Vector3 moveDirection = (nextPosition - transform.position).normalized;

                _moveBase.SetMoveDirection(moveDirection);

                if (Vector2.Distance(transform.position, nextPosition) <= _positionReachedDistance)
                {
                    _waypointIndex++;

                    if (_waypointIndex >= _waypoints.Length)
                    {
                        StopMovement();
                    }
                }
            }
            else
            {
                StopMovement();
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

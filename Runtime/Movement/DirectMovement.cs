using UnityEngine;

namespace KaynirGames.Movement
{
    public class DirectMovement : BaseMovement
    {
        [SerializeField] private CharacterMoveBase _characterMoveMethod = null;

        private Vector3 _movePosition;
        private Vector3 _moveDirection = Vector3.zero;

        private void Update()
        {
            if (_characterMoveMethod != null)
            {
                HandleMovement();
            }
        }

        public override void SetMovementPosition(Vector3 position)
        {
            _movePosition = position;
            _moveDirection = Vector3.one;
            ReachedDestination = false;
        }

        protected override void HandleMovement()
        {
            if (_moveDirection != Vector3.zero)
            {
                _moveDirection = (_movePosition - transform.position).normalized;
                _characterMoveMethod.SetMoveDirection(_moveDirection);

                if (Vector2.Distance(_movePosition, transform.position) <= _positionReachedDistance)
                {
                    _moveDirection = Vector3.zero;
                    ReachedDestination = true;
                }
            }
            else
            {
                _characterMoveMethod.SetMoveDirection(Vector3.zero);
            }
        }
    }
}

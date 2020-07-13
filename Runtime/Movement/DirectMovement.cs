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
            InPosition = false;
        }

        public override void StopMovement() => _moveDirection = Vector3.zero;

        protected override void HandleMovement()
        {
            if (_moveDirection != Vector3.zero)
            {
                _moveDirection = (_movePosition - transform.position).normalized;
                _characterMoveMethod.SetMoveDirection(_moveDirection);

                if (Vector2.Distance(_movePosition, transform.position) <= _positionReachedDistance)
                {
                    _moveDirection = Vector3.zero;
                    InPosition = true;
                }
            }
            else
            {
                _characterMoveMethod.SetMoveDirection(Vector3.zero);
            }
        }
    }
}

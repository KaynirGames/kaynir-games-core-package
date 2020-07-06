using UnityEngine;

namespace KaynirGames.Movement
{
    public class DirectMovement : BaseMovement
    {
        [SerializeField] private CharacterMoveBase _characterMove = null;

        private Vector3 _movePosition;
        private Vector3 _moveDirection = Vector3.zero;

        private void Update()
        {
            HandleMovement();
        }

        public override void SetMovementPosition(Vector3 position)
        {
            _movePosition = position;
            _moveDirection = Vector3.one;
        }

        protected override void HandleMovement()
        {
            if (_moveDirection != Vector3.zero)
            {
                _moveDirection = (_movePosition - transform.position).normalized;
                _characterMove.SetMoveDirection(_moveDirection);

                if (Vector2.Distance(_movePosition, transform.position) <= .05f)
                {
                    _moveDirection = Vector3.zero;
                }
            }
            else
            {
                _characterMove.SetMoveDirection(Vector3.zero);
            }
        }
    }
}

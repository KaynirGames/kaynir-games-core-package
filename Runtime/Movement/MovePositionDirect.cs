using UnityEngine;

namespace KaynirGames.Movement
{
    public class MovePositionDirect : MovePositionBase
    {
        private Vector3 _movePosition;
        private Vector3 _moveDirection = Vector3.zero;

        private void Update()
        {
            MovePosition();
        }

        public override void SetPosition(Vector3 position)
        {
            _movePosition = position;
            _moveDirection = Vector3.one;
            IsMoving = true;
        }

        public override void StopMovement()
        {
            _moveDirection = Vector3.zero;
            _moveBase.SetMoveDirection(Vector3.zero);
            IsMoving = false;
        }

        protected override void MovePosition()
        {
            if (_moveDirection != Vector3.zero)
            {
                _moveDirection = (_movePosition - transform.position).normalized;
                _moveBase.SetMoveDirection(_moveDirection);

                if (Vector2.Distance(_movePosition, transform.position) <= _positionReachedDistance)
                {
                    StopMovement();
                }
            }
            else
            {
                StopMovement();
            }
        }
    }
}

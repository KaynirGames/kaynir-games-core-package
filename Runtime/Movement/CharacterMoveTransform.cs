using UnityEngine;

namespace KaynirGames.Movement
{
    public class CharacterMoveTransform : CharacterMoveBase
    {
        [SerializeField] private float _moveSpeed = 30f;

        private Vector3 _moveDirection = Vector3.zero;

        private void Update()
        {
            HandleMove();
        }

        public override void SetMoveDirection(Vector3 moveDirection) => _moveDirection = moveDirection;

        public override void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

        public override void Toggle(bool enable)
        {
            enabled = enable;
        }

        protected override void HandleMove()
        {
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }
    }
}

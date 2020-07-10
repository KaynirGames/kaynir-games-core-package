using UnityEngine;

namespace KaynirGames.Movement
{
    public class CharacterMoveTransform : CharacterMoveBase
    {
        [SerializeField] private float _moveSpeed = 30f; // Скорость движения персонажа.

        private Vector3 _moveDirection = Vector3.zero; // Направление движения персонажа.

        private void Update()
        {
            HandleMove();
        }

        public override void SetMoveDirection(Vector3 moveDirection) => _moveDirection = moveDirection;

        public override void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

        public override void Enable()
        {
            enabled = true;
        }

        public override void Disable()
        {
            enabled = false;
        }

        protected override void HandleMove()
        {
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }
    }
}

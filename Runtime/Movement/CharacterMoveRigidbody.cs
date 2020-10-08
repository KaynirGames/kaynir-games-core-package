using UnityEngine;

namespace KaynirGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMoveRigidbody : CharacterMoveBase
    {
        [SerializeField] private float _moveSpeed = 25f; // Скорость движения персонажа.
        [SerializeField, Range(0f, 0.3f)] private float _moveSmoothing = .05f; // Сглаживание движения персонажа.
        [SerializeField] private bool _enableSmoothing = false; // Использовать сглаживание движения?

        private Vector3 _moveDirection = Vector3.zero; // Направление движения персонажа.
        private Rigidbody2D _characterRigidbody;
        private Vector2 _currentVelocity; // Модифицируется сглаживанием.

        private void Awake()
        {
            _characterRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            HandleMove();
        }

        public override void SetMoveDirection(Vector3 moveDirection) => _moveDirection = moveDirection;

        public override void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

        public override void Toggle(bool enable)
        {
            if (!enable)
            {
                _characterRigidbody.velocity = Vector2.zero;
                _moveDirection = Vector3.zero;
            }

            enabled = enable;
        }

        protected override void HandleMove()
        {
            Vector2 targetVelocity = _moveDirection * _moveSpeed * 10f * Time.fixedDeltaTime;
            _characterRigidbody.velocity = _enableSmoothing 
                ? targetVelocity 
                : Vector2.SmoothDamp(_characterRigidbody.velocity, targetVelocity, ref _currentVelocity, _moveSmoothing);
        }
    }
}

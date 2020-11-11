using UnityEngine;

namespace KaynirGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveRigidbody : MoveBase
    {
        [SerializeField, Range(0f, 0.3f)] private float _moveSmoothing = .05f;
        [SerializeField] private bool _enableSmoothing = false;

        private Rigidbody2D _rigidbody;
        private Vector2 _currentVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        protected override void Move()
        {
            Vector2 targetVelocity = _moveDirection * _moveSpeed * 10f * Time.fixedDeltaTime;

            _rigidbody.velocity = _enableSmoothing
                ? targetVelocity
                : Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _currentVelocity, _moveSmoothing);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _rigidbody.velocity = Vector3.zero;
        }
    }
}

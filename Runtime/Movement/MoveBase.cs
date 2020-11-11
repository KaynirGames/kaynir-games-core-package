using UnityEngine;

namespace KaynirGames.Movement
{
    public abstract class MoveBase : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed = 25f;

        protected Vector3 _moveDirection = Vector3.zero;

        public virtual void SetMoveDirection(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        public virtual void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        protected abstract void Move();

        protected virtual void OnDisable()
        {
            _moveDirection = Vector3.zero;
        }
    }
}

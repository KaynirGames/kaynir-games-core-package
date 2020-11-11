using UnityEngine;

namespace KaynirGames.Movement
{
    public abstract class MovePositionBase : MonoBehaviour
    {
        [SerializeField] protected float _positionReachedDistance = .05f;

        protected MoveBase _moveBase;

        protected virtual void Awake()
        {
            _moveBase = GetComponent<MoveBase>();
        }

        public bool IsMoving { get; protected set; }

        public abstract void SetPosition(Vector3 position);

        public abstract void StopMovement();

        protected abstract void MovePosition();

        protected virtual void OnDisable()
        {
            StopMovement();
        }
    }
}

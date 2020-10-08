using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Основа вида перемещения.
    /// </summary>
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] protected float _positionReachedDistance = .05f;

        public bool IsMoving { get; protected set; }

        public abstract void SetMovementPosition(Vector3 position);

        public abstract void StopMovement();

        protected abstract void HandleMovement();
    }
}

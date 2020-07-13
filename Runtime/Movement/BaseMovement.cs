using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Основа вида перемещения.
    /// </summary>
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] protected float _positionReachedDistance = .05f; // Дистанция, на которой позиция считается достигнутой.
        /// <summary>
        /// Достигли назначенной позиции?
        /// </summary>
        public bool InPosition { get; protected set; }
        /// <summary>
        /// Задать позицию для перемещения.
        /// </summary>
        public abstract void SetMovementPosition(Vector3 position);
        /// <summary>
        /// Остановить перемещение.
        /// </summary>
        public abstract void StopMovement();
        /// <summary>
        /// Осуществить перемещение.
        /// </summary>
        protected abstract void HandleMovement();
    }
}

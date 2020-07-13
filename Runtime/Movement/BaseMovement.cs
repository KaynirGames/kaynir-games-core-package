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
        /// Достигли точки назначения?
        /// </summary>
        public bool ReachedDestination { get; protected set; }
        /// <summary>
        /// Задать позицию для перемещения.
        /// </summary>
        public abstract void SetMovementPosition(Vector3 position);
        /// <summary>
        /// Осуществить перемещение.
        /// </summary>
        protected abstract void HandleMovement();
    }
}

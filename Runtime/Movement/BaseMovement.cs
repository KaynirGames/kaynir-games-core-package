using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Основа вида перемещения.
    /// </summary>
    public abstract class BaseMovement : MonoBehaviour
    {
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

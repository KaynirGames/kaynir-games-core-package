using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Основа механики движения персонажа.
    /// </summary>
    public abstract class CharacterMoveBase : MonoBehaviour
    {
        /// <summary>
        /// Задать направление движения персонажа.
        /// </summary>
        public abstract void SetMoveDirection(Vector3 moveDirection);
        /// <summary>
        /// Задать скорость движения персонажа.
        /// </summary>
        public abstract void SetMoveSpeed(float moveSpeed);
        /// <summary>
        /// Активировать возможность перемещения.
        /// </summary>
        public abstract void Enable();
        /// <summary>
        /// Деактивировать возможность перемещения.
        /// </summary>
        public abstract void Disable();
        /// <summary>
        /// Осуществить движение персонажа.
        /// </summary>
        protected abstract void HandleMove();
    }
}

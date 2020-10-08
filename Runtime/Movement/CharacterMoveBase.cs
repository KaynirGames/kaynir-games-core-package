using UnityEngine;

namespace KaynirGames.Movement
{
    /// <summary>
    /// Основа механики движения персонажа.
    /// </summary>
    public abstract class CharacterMoveBase : MonoBehaviour
    {

        public abstract void SetMoveDirection(Vector3 moveDirection);

        public abstract void SetMoveSpeed(float moveSpeed);

        public abstract void Toggle(bool enable);

        protected abstract void HandleMove();
    }
}

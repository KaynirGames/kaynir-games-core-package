using UnityEngine;

namespace KaynirGames.InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] protected bool _snapMovementAxes = true;

        public virtual Vector3 GetMovementInput()
        {
            Vector3 direction = Vector3.zero;

            direction.x = GetAxisValue("Horizontal");
            direction.y = GetAxisValue("Vertical");

            return Vector3.ClampMagnitude(direction, 1f);
        }

        public virtual bool GetAttackInput()
        {
            return Input.GetButtonDown("Attack");
        }

        private float GetAxisValue(string axisName)
        {
            return _snapMovementAxes
                ? Input.GetAxisRaw(axisName)
                : Input.GetAxis(axisName);
        }
    }
}
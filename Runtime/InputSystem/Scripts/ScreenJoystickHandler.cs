using UnityEngine;

namespace KaynirGames.InputSystem
{
    public class ScreenJoystickHandler : InputHandler
    {
        [SerializeField] private Joystick _joystick = null;
        [SerializeField] private ScreenJoybutton _attackButton = null;

        private void Awake()
        {
            if (_snapMovementAxes)
            {
                _joystick.SnapX = true;
                _joystick.SnapY = true;
            }
        }

        public override Vector3 GetMovementInput()
        {
            Vector3 direction = Vector3.zero;

            direction.x = _joystick.Horizontal;
            direction.y = _joystick.Vertical;

            return Vector3.ClampMagnitude(direction, 1f);
        }

        public override bool GetAttackInput()
        {
            return _attackButton.IsPressed;
        }

        public void SetJoystick(Joystick joystick, ScreenJoybutton joybutton)
        {
            _joystick = joystick;
            _attackButton = joybutton;
        }
    }
}

using KaynirGames.InputSystem;
using KaynirGames.Movement;
using UnityEngine;

public class ControlsTest : MonoBehaviour
{
    [SerializeField] private CharacterMoveBase _characterMoveBase = null;
    [SerializeField] private InputHandler _inputControl = null;

    private Vector3 _moveDirection = Vector3.zero;

    private void Update()
    {
        _moveDirection = _inputControl.GetMovementInput();
        _characterMoveBase.SetMoveDirection(_moveDirection);

        if (_inputControl.GetAttackInput())
        {
            Debug.Log("Attacking!");
        }

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Attack button pressed.");
        }
    }
}
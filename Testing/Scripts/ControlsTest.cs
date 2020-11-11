using KaynirGames.InputSystem;
using KaynirGames.Movement;
using UnityEngine;

public class ControlsTest : MonoBehaviour
{
    [SerializeField] private MoveBase _characterMoveBase = null;
    [SerializeField] private InputHandler _inputControl = null;

    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoveEnabled = true;

    private void Update()
    {
        _moveDirection = _inputControl.GetMovementInput();
        _characterMoveBase.SetMoveDirection(_moveDirection);

        if (_inputControl.GetAttackInput())
        {
            Debug.Log("Attacking!");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMoveEnabled = !_isMoveEnabled;
            _characterMoveBase.enabled = _isMoveEnabled;
        }
    }
}
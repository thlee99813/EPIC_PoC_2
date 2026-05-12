using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;

    public Vector2 MoveInput { get; private set; }

    private bool _jumpPressed;

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _jumpAction.action.Disable();
    }

    private void Update()
    {
        MoveInput = _moveAction.action.ReadValue<Vector2>();

        if (_jumpAction.action.WasPressedThisFrame())
        {
            _jumpPressed = true;
        }
    }

    public bool ConsumeJumpPressed()
    {
        if (!_jumpPressed)
        {
            return false;
        }

        _jumpPressed = false;
        return true;
    }
}

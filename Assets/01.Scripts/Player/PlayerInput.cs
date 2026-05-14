using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _umbrellaAction;
    [SerializeField] private InputActionReference _attackAction;


    public Vector2 MoveInput { get; private set; }
    public bool IsUmbrellaHeld { get; private set; }
    public bool IsJumpHeld { get; private set; }



    private bool _jumpPressed;
    private bool _attackPressed;

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
        _umbrellaAction.action.Enable();
        _attackAction.action.Enable();

    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _jumpAction.action.Disable();
        _umbrellaAction.action.Disable();
        _attackAction.action.Disable();

    }

    private void Update()
    {
        MoveInput = _moveAction.action.ReadValue<Vector2>();
        IsUmbrellaHeld = _umbrellaAction.action.IsPressed();
        IsJumpHeld = _jumpAction.action.IsPressed();


        if (_jumpAction.action.WasPressedThisFrame())
        {
            _jumpPressed = true;
        }
        if (_attackAction.action.WasPressedThisFrame())
        {
            _attackPressed = true;
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
    public bool ConsumeAttackPressed()
    {
        if (!_attackPressed)
        {
            return false;
        }

        _attackPressed = false;
        return true;
    }
    
}

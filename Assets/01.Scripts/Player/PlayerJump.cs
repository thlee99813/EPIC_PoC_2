using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 12f;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private PlayerGroundChecker _groundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundChecker = GetComponent<PlayerGroundChecker>();
    }

    public bool TryJump()
    {
        if (!_playerInput.ConsumeJumpPressed())
        {
            return false;
        }

        if (!_groundChecker.IsGrounded)
        {
            return false;
        }

        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            _jumpForce
        );

        return true;
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private PlayerStats _playerStats;
    private PlayerInput _playerInput;
    private PlayerGroundChecker _groundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
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

        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _playerStats.JumpForce);

        return true;
    }
}

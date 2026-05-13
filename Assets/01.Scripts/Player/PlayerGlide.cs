using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundChecker))]

public class PlayerGlide : MonoBehaviour
{
    [SerializeField] private float _maxFallSpeed = -2.5f;
    [SerializeField] private float _glideGravityScale = 0.35f;
    [SerializeField] private float _minHorizontalSpeedToGlide = 4f;


    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private PlayerGroundChecker _groundChecker;

    private float _defaultGravityScale;

    public bool IsGliding { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundChecker = GetComponent<PlayerGroundChecker>();
        _defaultGravityScale = _rigidbody.gravityScale;
    }

    private void FixedUpdate()
    {
        bool hasEnoughHorizontalSpeed = Mathf.Abs(_rigidbody.linearVelocity.x) >= _minHorizontalSpeedToGlide;
        bool canGlide = !_groundChecker.IsGrounded && _playerInput.IsJumpHeld && _rigidbody.linearVelocity.y < 0f && hasEnoughHorizontalSpeed;

        if (!canGlide)
        {
            StopGlide();
            return;
        }

        StartGlide();
    }

    private void StartGlide()
    {
        IsGliding = true;
        _rigidbody.gravityScale = _glideGravityScale;

        float nextY = Mathf.Max(_rigidbody.linearVelocity.y, _maxFallSpeed);
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, nextY);
    }


    private void StopGlide()
    {
        if (!IsGliding)
        {
            return;
        }

        IsGliding = false;
        _rigidbody.gravityScale = _defaultGravityScale;
    }
}

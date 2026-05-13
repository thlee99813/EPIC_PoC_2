using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundChecker))]

public class PlayerGlide : MonoBehaviour
{
    [SerializeField] private float _maxFallSpeed = -2.5f;
    [SerializeField] private float _glideGravityScale = 0.35f;
    [SerializeField] private float _minGlideForwardSpeed = 2f;


    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private PlayerGroundChecker _groundChecker;
    private PlayerMove _playerMove;


    private float _defaultGravityScale;

    public bool IsGliding { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundChecker = GetComponent<PlayerGroundChecker>();
        _playerMove = GetComponent<PlayerMove>();
        _defaultGravityScale = _rigidbody.gravityScale;
    }

    private void FixedUpdate()
    {
        bool canGlide = !_groundChecker.IsGrounded && _playerInput.IsJumpHeld && _rigidbody.linearVelocity.y < 0f;


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

        float nextX = _rigidbody.linearVelocity.x;

        if (Mathf.Abs(nextX) < _minGlideForwardSpeed)
        {
            nextX = _playerMove.FacingDirection * _minGlideForwardSpeed;
        }

        float nextY = Mathf.Max(_rigidbody.linearVelocity.y, _maxFallSpeed);
        _rigidbody.linearVelocity = new Vector2(nextX, nextY);
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

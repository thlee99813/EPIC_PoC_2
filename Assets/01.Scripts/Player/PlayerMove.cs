using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundChecker))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerSlopeSlide))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _groundAcceleration = 40f;
    [SerializeField] private float _groundDeceleration = 12f;
    [SerializeField] private float _airControlAcceleration = 8f;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private PlayerGroundChecker _groundChecker;
    private PlayerJump _playerJump;
    private PlayerSlopeSlide _slopeSlide;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundChecker = GetComponent<PlayerGroundChecker>();
        _playerJump = GetComponent<PlayerJump>();
        _slopeSlide = GetComponent<PlayerSlopeSlide>();
    }

    private void FixedUpdate()
    {
        _groundChecker.Refresh();

        bool jumpedThisFrame = _playerJump.TryJump();

        if (!jumpedThisFrame)
        {
            MoveHorizontal();
            _slopeSlide.Slide();
        }
    }

   private void MoveHorizontal()
    {
    bool useGroundMove = _groundChecker.IsGrounded && !_slopeSlide.IsSlidingSlope;

        if (useGroundMove)
        {
            float targetX = _playerInput.MoveInput.x * _moveSpeed;
            float acceleration = Mathf.Abs(_playerInput.MoveInput.x) > 0.01f ? _groundAcceleration : _groundDeceleration;
            float nextX = Mathf.MoveTowards(_rigidbody.linearVelocity.x, targetX, acceleration * Time.fixedDeltaTime);

            _rigidbody.linearVelocity = new Vector2(nextX, _rigidbody.linearVelocity.y);
            return;
        }

        if (Mathf.Abs(_playerInput.MoveInput.x) < 0.01f)
        {
            return;
        }

        float airTargetX = _playerInput.MoveInput.x * _moveSpeed;
        float airNextX = Mathf.MoveTowards(_rigidbody.linearVelocity.x, airTargetX, _airControlAcceleration * Time.fixedDeltaTime);

        _rigidbody.linearVelocity = new Vector2(airNextX, _rigidbody.linearVelocity.y);
    }

}

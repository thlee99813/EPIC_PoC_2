using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerSlopeSlide : MonoBehaviour
{
    [SerializeField] private float _slideStartAngle = 12f;

    private Rigidbody2D _rigidbody;
    private PlayerStats _playerStats;
    private PlayerGroundChecker _groundChecker;

    public bool IsSlidingSlope =>
        _groundChecker.HasGroundHit &&
        _groundChecker.SlopeAngle >= _slideStartAngle;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
        _groundChecker = GetComponent<PlayerGroundChecker>();

    }

    public void Slide()
    {
        if (!IsSlidingSlope)
        {
            return;
        }

        Vector2 slideDirection = new Vector2(
            _groundChecker.GroundNormal.y,
            -_groundChecker.GroundNormal.x
        );

        if (slideDirection.y > 0f)
        {
            slideDirection = -slideDirection;
        }

        Vector2 targetVelocity = slideDirection.normalized * _playerStats.SlideSpeed;
        _rigidbody.linearVelocity = Vector2.MoveTowards(_rigidbody.linearVelocity, targetVelocity, _playerStats.SlideAcceleration * Time.fixedDeltaTime);

    }
}

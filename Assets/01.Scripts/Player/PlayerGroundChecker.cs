using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.12f;
    [SerializeField] private float _slopeCheckDistance = 0.35f;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }
    public bool HasGroundHit { get; private set; }
    public Vector2 GroundNormal { get; private set; } = Vector2.up;
    public float SlopeAngle { get; private set; }

    public void Refresh()
    {
        IsGrounded = Physics2D.OverlapCircle(
            _groundCheck.position,
            _groundCheckRadius,
            _groundLayer
        );

        RaycastHit2D hit = Physics2D.Raycast(
            _groundCheck.position,
            Vector2.down,
            _slopeCheckDistance,
            _groundLayer
        );

        HasGroundHit = hit;

        if (!HasGroundHit)
        {
            GroundNormal = Vector2.up;
            SlopeAngle = 0f;
            return;
        }

        GroundNormal = hit.normal;
        SlopeAngle = Vector2.Angle(hit.normal, Vector2.up);
    }
}

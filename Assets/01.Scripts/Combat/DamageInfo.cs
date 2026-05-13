using UnityEngine;

public readonly struct DamageInfo
{
    public readonly float Amount;
    public readonly Vector2 HitPoint;
    public readonly Vector2 HitDirection;

    public DamageInfo(float amount, Vector2 hitPoint, Vector2 hitDirection)
    {
        Amount = amount;
        HitPoint = hitPoint;
        HitDirection = hitDirection;
    }
}

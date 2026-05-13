using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private LayerMask _targetLayer;

    private EnemyStats _enemyStats;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction, _projectileSpeed, _enemyStats.AttackPower, transform, _targetLayer);
    }

}

using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerUmbrella))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerUmbrellaShooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 12f;
    [SerializeField] private LayerMask _targetLayer;


    private PlayerInput _playerInput;
    private PlayerUmbrella _playerUmbrella;
    private PlayerStats _playerStats;
    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerUmbrella = GetComponent<PlayerUmbrella>();
        _playerStats = GetComponent<PlayerStats>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (!_playerInput.ConsumeAttackPressed())
        {
            return;
        }

        if (!_playerUmbrella.IsBlockingProjectile)
        {
            return;
        }
        Shoot();
    }

    private void Shoot()
    {
        Vector2 direction = new Vector2(_playerMove.FacingDirection, 0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, direction.x > 0f ? 0f : 180f);

        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction, _projectileSpeed, _playerStats.AttackPower, transform, _targetLayer);
    }

}

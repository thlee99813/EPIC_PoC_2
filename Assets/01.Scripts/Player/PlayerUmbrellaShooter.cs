using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerUmbrellaShooter : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 12f;
    [SerializeField] private float _spCost = 20f;
    [SerializeField] private float _damagePerSP = 0.5f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _thinLaserDamage = 40f;
    [SerializeField] private float _thinLaserRadius = 0.15f;
    [SerializeField] private float _largeLaserDamage = 100f;
    [SerializeField] private float _largeLaserRadius = 0.45f;
    [SerializeField] private float _laserDistance = 20f;
    [SerializeField] private LayerMask _laserTargetLayer;
    [SerializeField] private Transform _laserVisual;
    [SerializeField] private float _laserShowTime = 0.1f;
    [SerializeField] private SpriteRenderer _laserSpriteRenderer;





    private PlayerStats _playerStats;
    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerMove = GetComponent<PlayerMove>();
        _laserVisual.gameObject.SetActive(false);
    }
   
    public void Shoot()
    {
        Vector2 direction = new Vector2(_playerMove.FacingDirection, 0f);

        if (_playerStats.CurrentSP >= 80f && _playerStats.GuardSP >= 40f)
        {
            _playerStats.SpendAllSP();
            ShootLaser(direction, _largeLaserDamage, _largeLaserRadius);
            return;
        }

        if (_playerStats.CurrentSP >= 50f && _playerStats.GuardSP >= 20f)
        {
            _playerStats.SpendAllSP();
            ShootLaser(direction, _thinLaserDamage, _thinLaserRadius);
            return;
        }

        if (!_playerStats.TrySpendSP(_spCost))
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, direction.x > 0f ? 0f : 180f);
        float damage = _playerStats.AttackPower + (_spCost * _damagePerSP);

        Projectile projectile = Instantiate(_projectilePrefab, _firePoint.position, rotation);
        projectile.Launch(direction, _projectileSpeed, damage, transform, _targetLayer);
    }
    private void ShootLaser(Vector2 direction, float damage, float radius)
    {
        _laserVisual.DOKill();
        _laserSpriteRenderer.DOKill();

        _laserVisual.gameObject.SetActive(true);

        Color color = _laserSpriteRenderer.color;
        color.a = 1f;
        _laserSpriteRenderer.color = color;

        float visualLength = _laserDistance * 10f;
        float visualWidth = radius * 4f;

        _laserVisual.localPosition = new Vector3(0f, visualLength * 0.5f, 0f);
        _laserVisual.localRotation = Quaternion.identity;
        _laserVisual.localScale = new Vector3(visualWidth, visualLength, 1f);


        _laserSpriteRenderer.DOFade(0f, _laserShowTime).OnComplete(() => _laserVisual.gameObject.SetActive(false));

        RaycastHit2D[] hits = Physics2D.CircleCastAll(_firePoint.position, radius, direction, visualLength, _laserTargetLayer);
        HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damageable = hits[i].collider.GetComponentInParent<IDamageable>();

            if (damageable == null || !damagedTargets.Add(damageable))
            {
                continue;
            }

            DamageInfo damageInfo = new DamageInfo(damage, hits[i].point, direction);
            damageable.TakeDamage(damageInfo);
        }
    }



}

using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerUmbrella))]

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform _playerHandRoot;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 0.8f;
    [SerializeField] private float _damageMultiplier = 2f;
    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private float _prepareAngle = 45f;
    [SerializeField] private float _strikeAngle = -90f;
    [SerializeField] private float _idleAngle = 20f;

    [SerializeField] private float _prepareDuration = 0.07f;
    [SerializeField] private float _strikeDuration = 0.09f;
    [SerializeField] private float _recoverDuration = 0.08f;

    private PlayerStats _playerStats;
    private PlayerUmbrella _playerUmbrella;

    private Sequence _attackSequence;
    private bool _isAttacking;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerUmbrella = GetComponent<PlayerUmbrella>();

    }

    private void OnDestroy()
    {
        _attackSequence?.Kill();
    }

    public bool TryAttack()
    {
        if (_isAttacking)
        {
            return false;
        }

        _isAttacking = true;
        _playerUmbrella.SetControlLocked(true);


        _attackSequence?.Kill();
        _attackSequence = DOTween.Sequence();

        _attackSequence.Append(_playerHandRoot.DOLocalRotate(new Vector3(0f, 0f, _prepareAngle), _prepareDuration).SetEase(Ease.OutQuad));
        _attackSequence.Append(_playerHandRoot.DOLocalRotate(new Vector3(0f, 0f, _strikeAngle), _strikeDuration).SetEase(Ease.InQuad));
        _attackSequence.AppendCallback(DamageTargets);
        _attackSequence.Append(_playerHandRoot.DOLocalRotate(new Vector3(0f, 0f, _idleAngle), _recoverDuration).SetEase(Ease.OutQuad));
        _attackSequence.OnComplete(() =>
        {
            _isAttacking = false;
            _playerUmbrella.SetControlLocked(false);
        });

        return true;
    }

    private void DamageTargets()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _targetLayer);
        List<IDamageable> damagedTargets = new();

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damageable = hits[i].GetComponentInParent<IDamageable>();

            if (damageable == null || damagedTargets.Contains(damageable))
            { 
                continue;
            }

            damagedTargets.Add(damageable);

            float damage = _playerStats.AttackPower * _damageMultiplier;
            DamageInfo damageInfo = new DamageInfo(damage, hits[i].transform.position, Vector2.zero);
            damageable.TakeDamage(damageInfo);
        }
    }
    private void SheildAction()
    {
        
    }
}

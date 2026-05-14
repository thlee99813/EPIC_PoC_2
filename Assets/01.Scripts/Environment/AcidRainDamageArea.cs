using System.Collections.Generic;
using UnityEngine;

public class AcidRainDamageArea : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;
    [SerializeField] private float _damageInterval = 0.1f;
    [SerializeField] private float _spGainWhenBlocked = 1f;
    [SerializeField] private LayerMask _rainBlockLayer;
    [SerializeField] private float _rainBlockCheckDistance = 30f;


    private readonly List<IDamageable> _targets = new();
    private float _damageTimer;

    private void Update()
    {
        if (_targets.Count == 0)
        {
            return;
        }

        _damageTimer += Time.deltaTime;

        if (_damageTimer < _damageInterval)
        {
            return;
        }

        _damageTimer = 0f;
        DamageTargets();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerStats playerStats))
        {
            return;
        }

        if (!_targets.Contains(playerStats))
        {
            _targets.Add(playerStats);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerStats playerStats))
        {
            return;
        }

        _targets.Remove(playerStats);
    }


    private void DamageTargets()
    {
        DamageInfo damageInfo = new DamageInfo(_damage, transform.position, Vector2.down);

        for (int i = _targets.Count - 1; i >= 0; i--)
        {
            IDamageable target = _targets[i];

        if (IsBlockedByRainBlocker(target))
        {
            continue;
        }

        if (IsBlockedByUmbrella(target))
        {
            if (target is PlayerStats playerStats)
            {
                playerStats.GainRainSP(_spGainWhenBlocked);
            }

            continue;
        }

        target.TakeDamage(damageInfo);



        }
    }
    private bool IsBlockedByRainBlocker(IDamageable target)
    {
        Component component = target as Component;

        if (component == null)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(component.transform.position, Vector2.up, _rainBlockCheckDistance, _rainBlockLayer);
        return hit.collider != null;
    }

    private bool IsBlockedByUmbrella(IDamageable target)
    {
        Component component = target as Component;

        if (component == null)
        {
            return false;
        }

        if (!component.TryGetComponent(out PlayerUmbrella umbrella))
        {
            return false;
        }

        return umbrella.IsBlockingRain;
    }

}

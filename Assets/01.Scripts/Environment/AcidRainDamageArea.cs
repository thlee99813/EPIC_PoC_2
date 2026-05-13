using System.Collections.Generic;
using UnityEngine;

public class AcidRainDamageArea : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;
    [SerializeField] private float _damageInterval = 0.1f;

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
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        if (!_targets.Contains(damageable))
        {
            _targets.Add(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        _targets.Remove(damageable);
    }

    private void DamageTargets()
    {
        DamageInfo damageInfo = new DamageInfo(_damage, transform.position, Vector2.down);

        for (int i = _targets.Count - 1; i >= 0; i--)
        {
            _targets[i].TakeDamage(damageInfo);
        }
    }
}

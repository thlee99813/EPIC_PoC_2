using System.Collections.Generic;
using UnityEngine;

public class WaterDamageArea : MonoBehaviour
{
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _damageInterval = 0.1f;

    private readonly List<PlayerStats> _targets = new();
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
        DamageInfo damageInfo = new DamageInfo(_damage, transform.position, Vector2.zero);

        for (int i = _targets.Count - 1; i >= 0; i--)
        {
            _targets[i].TakeDamage(damageInfo);
        }
    }
}

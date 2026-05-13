using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStatData _statData;

    private float _currentHealth;
    private bool _isDead;

    public event Action<float, float> HealthChanged;
    public event Action Died;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _statData.MaxHealth;
    public float AttackPower => _statData.AttackPower;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, MaxHealth);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (_isDead)
        {
            return;
        }

        _currentHealth = Mathf.Max(_currentHealth - damageInfo.Amount, 0f);
        HealthChanged?.Invoke(_currentHealth, MaxHealth);

        if (_currentHealth <= 0f)
        {
            _isDead = true;
            Died?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

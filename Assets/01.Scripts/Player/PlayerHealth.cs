using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    public event Action<int, int> HealthChanged;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public float NormalizedHealth => (float)_currentHealth / _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        _currentHealth = Mathf.Max(_currentHealth - damageInfo.Amount, 0);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }


    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}

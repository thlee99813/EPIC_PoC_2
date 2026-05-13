using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStatData _statData;

    private float _currentHealth;
    private bool _isDead;

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _statData.MaxHealth;
    public int AttackPower => _statData.AttackPower;
    public float MoveSpeed => _statData.MoveSpeed;
    public float JumpForce => _statData.JumpForce;
    public float SlideSpeed => _statData.SlideSpeed;
    public float SlideAcceleration => _statData.SlideAcceleration;
    public float RainResistance => _statData.RainResistance;
    public float NormalizedHealth => (float)_currentHealth / MaxHealth;
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

        _currentHealth = Mathf.Max(_currentHealth - damageInfo.Amount, 0);
        HealthChanged?.Invoke(_currentHealth, MaxHealth);

        if (_currentHealth <= 0)
        {
            _isDead = true;
            Died?.Invoke();
        }
    }


    public void Heal(int amount)
    {
        if(_isDead)
        {
            return;
        }
        _currentHealth = Mathf.Min(_currentHealth + amount, MaxHealth);
        HealthChanged?.Invoke(_currentHealth, MaxHealth);
    }
}

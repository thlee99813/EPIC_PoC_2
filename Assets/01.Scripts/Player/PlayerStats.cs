using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStatData _statData;

    private float _currentHealth;
    private float _rainSP;
    private float _guardSP;

    private bool _isDead;

    public event Action<float, float> HealthChanged;
    public event Action<float, float, float> SPChanged;
    public event Action Died;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _statData.MaxHealth;
    public float RainSP => _rainSP;
    public float GuardSP => _guardSP;
    public float CurrentSP => _rainSP + _guardSP;
    public float MaxSP => _statData.MaxSP;
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
        _rainSP = 0f;
        _guardSP = 0f;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, MaxHealth);
        SPChanged?.Invoke(_rainSP, _guardSP, MaxSP);
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

    public void GainRainSP(float amount)
    {
        AddSP(ref _rainSP, amount);
    }

    public void GainGuardSP(float amount)
    {
        AddSP(ref _guardSP, amount);
    }

    public bool TrySpendSP(float amount)
    {
        if (CurrentSP < amount)
        {
            return false;
        }

        SpendSP(amount);
        SPChanged?.Invoke(_rainSP, _guardSP, MaxSP);
        return true;
    }

    public float SpendAllSP()
    {
        float spentSP = CurrentSP;

        _rainSP = 0f;
        _guardSP = 0f;

        SPChanged?.Invoke(_rainSP, _guardSP, MaxSP);
        return spentSP;
    }

    private void AddSP(ref float sp, float amount)
    {
        float addableAmount = Mathf.Min(amount, MaxSP - CurrentSP);
        sp += addableAmount;
        SPChanged?.Invoke(_rainSP, _guardSP, MaxSP);
    }

    private void SpendSP(float amount)
    {
        float rainSpendAmount = Mathf.Min(_rainSP, amount);
        _rainSP -= rainSpendAmount;
        amount -= rainSpendAmount;

        _guardSP = Mathf.Max(0f, _guardSP - amount);
    }


}

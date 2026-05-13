using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private PlayerStats _playerStats;

    private void OnDisable()
    {
        if (_playerStats != null)
        {
            _playerStats.HealthChanged -= Refresh;
        }
    }

    public void Bind(PlayerStats playerStats)
    {
        if (_playerStats != null)
        {
            _playerStats.HealthChanged -= Refresh;
        }

        _playerStats = playerStats;
        _playerStats.HealthChanged += Refresh;

        _healthSlider.minValue = 0f;
        _healthSlider.maxValue = 1f;

        Refresh(_playerStats.CurrentHealth, _playerStats.MaxHealth);
    }

    private void Refresh(float currentHealth, float maxHealth)
    {
        _healthSlider.value = (float)currentHealth / maxHealth;
    }
}

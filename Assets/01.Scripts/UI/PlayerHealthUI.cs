using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private PlayerHealth _playerHealth;

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.HealthChanged -= Refresh;
        }
    }

    public void Bind(PlayerHealth playerHealth)
    {
        if (_playerHealth != null)
        {
            _playerHealth.HealthChanged -= Refresh;
        }

        _playerHealth = playerHealth;
        _playerHealth.HealthChanged += Refresh;

        _healthSlider.minValue = 0f;
        _healthSlider.maxValue = 1f;

        Refresh(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
    }

    private void Refresh(int currentHealth, int maxHealth)
    {
        _healthSlider.value = (float)currentHealth / maxHealth;
    }
}

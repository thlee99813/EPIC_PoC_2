using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarView : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private EnemyHealthBarTarget _target;
    private Camera _worldCamera;

    public void Bind(EnemyHealthBarTarget target, Camera worldCamera)
    {
        _target = target;
        _worldCamera = worldCamera;

        _target.EnemyStats.HealthChanged += Refresh;
        _target.EnemyStats.Died += Hide;

        Refresh(_target.EnemyStats.CurrentHealth, _target.EnemyStats.MaxHealth);
        gameObject.SetActive(true);
    }

    public void Unbind()
    {
        if (_target == null)
        {
            return;
        }

        _target.EnemyStats.HealthChanged -= Refresh;
        _target.EnemyStats.Died -= Hide;
        _target = null;
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = _worldCamera.WorldToScreenPoint(_target.WorldPosition);
    }

    private void Refresh(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth / maxHealth;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

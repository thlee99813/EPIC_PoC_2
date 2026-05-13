using UnityEngine;

[RequireComponent(typeof(EnemyHealthBarTarget))]
public class EnemyHealthBarRegister : MonoBehaviour
{
    private EnemyHealthBarTarget _target;
    private EnemyHealthBarLayer _healthBarLayer;

    private void Awake()
    {
        _target = GetComponent<EnemyHealthBarTarget>();
    }

    private void OnDisable()
    {
        if (_healthBarLayer == null) return;

        _healthBarLayer.Unregister(_target);
    }

    public void Initialize(EnemyHealthBarLayer healthBarLayer)
    {
        _healthBarLayer = healthBarLayer;
        _healthBarLayer.Register(_target);
    }
}

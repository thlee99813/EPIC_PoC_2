using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarLayer : MonoBehaviour
{
    [SerializeField] private Camera _worldCamera;
    [SerializeField] private EnemyHealthBarView _healthBarPrefab;

    private readonly Dictionary<EnemyHealthBarTarget, EnemyHealthBarView> _views = new();

    public void Register(EnemyHealthBarTarget target)
    {
        if (_views.ContainsKey(target))
        {
            return;
        }

        EnemyHealthBarView view = Instantiate(_healthBarPrefab, transform);
        view.Bind(target, _worldCamera);
        _views.Add(target, view);
    }

    public void Unregister(EnemyHealthBarTarget target)
    {
        if (!_views.TryGetValue(target, out EnemyHealthBarView view))
        {
            return;
        }

        view.Unbind();
        Destroy(view.gameObject);
        _views.Remove(target);
    }
}

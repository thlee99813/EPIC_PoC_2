using UnityEngine;

public class EnemySceneBinder : MonoBehaviour
{
    [SerializeField] private EnemyHealthBarLayer _healthBarLayer;
    [SerializeField] private EnemyHealthBarRegister[] _enemies;

    private void Start()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i].Initialize(_healthBarLayer);
        }
    }
}

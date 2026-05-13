using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealthBarLayer _healthBarLayer;
    [SerializeField] private EnemyHealthBarRegister _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;

    public void Spawn()
    {
        EnemyHealthBarRegister enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        enemy.Initialize(_healthBarLayer);
    }
}

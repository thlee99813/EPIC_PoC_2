using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyRangedAttack))]
public class EnemyController : MonoBehaviour
{
    private EnemyStats _enemyStats;
    private EnemyRangedAttack _rangedAttack;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _rangedAttack = GetComponent<EnemyRangedAttack>();
    }

    private void Update()
    {
        _rangedAttack.SetActive(!_enemyStats.IsDead);
    }
}

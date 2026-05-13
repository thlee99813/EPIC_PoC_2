using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatData", menuName = "Rainy Dancer/Enemy Stat Data")]
public class EnemyStatData : ScriptableObject
{
    [SerializeField] private float _maxHealth = 30f;
    [SerializeField] private float _attackPower = 5f;

    public float MaxHealth => _maxHealth;
    public float AttackPower => _attackPower;
}

using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyHealthBarTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 1.2f, 0f);

    private EnemyStats _enemyStats;

    public EnemyStats EnemyStats => _enemyStats;
    public Vector3 WorldPosition => _target.position + _offset;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }
}

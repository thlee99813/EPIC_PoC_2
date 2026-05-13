using UnityEngine;

[RequireComponent(typeof(EnemyShooter))]
public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _attackRange = 15f;
    [SerializeField] private float _attackInterval = 1.5f;

    private EnemyShooter _enemyShooter;
    private float _attackTimer;
    private bool _isActive = true;

    private void Awake()
    {
        _enemyShooter = GetComponent<EnemyShooter>();
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (!IsTargetInRange())
        {
            return;
        }

        _attackTimer += Time.deltaTime;

        if (_attackTimer < _attackInterval)
        {
            return;
        }

        _attackTimer = 0f;
        Attack();
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    private void Attack()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        _enemyShooter.Shoot(direction);
    }

    private bool IsTargetInRange()
    {
        float distance = Vector2.Distance(transform.position, _target.position);
        return distance <= _attackRange;
    }
}

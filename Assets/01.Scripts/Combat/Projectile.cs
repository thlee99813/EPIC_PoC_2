using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;

    private Rigidbody2D _rigidbody;
    private Transform _owner;
    private LayerMask _targetLayer;
    private float _damage;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void Launch(Vector2 direction, float speed, float damage, Transform owner, LayerMask targetLayer)
    {
        _owner = owner;
        _targetLayer = targetLayer;
        _damage = damage;
        _rigidbody.linearVelocity = direction.normalized * speed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_owner != null && other.transform.root == _owner.root)
        {
            return;
        }

        if (other.TryGetComponent(out UmbrellaBlocker blocker) && blocker.TryBlock())
        {
            Destroy(gameObject);
            return;
        }

        if (!IsInTargetLayer(other.gameObject.layer))
        {
            return;
        }

        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        DamageInfo damageInfo = new DamageInfo(_damage, transform.position, _rigidbody.linearVelocity.normalized);
        damageable.TakeDamage(damageInfo);

        Destroy(gameObject);
    }

    private bool IsInTargetLayer(int layer)
    {
        return (_targetLayer.value & (1 << layer)) != 0;
    }

}

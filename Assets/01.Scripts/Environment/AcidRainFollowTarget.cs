using UnityEngine;

public class AcidRainFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset = new Vector2(0f, 5f);

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position;
        transform.position = new Vector3(targetPosition.x + _offset.x, targetPosition.y + _offset.y, transform.position.z);
    }
}

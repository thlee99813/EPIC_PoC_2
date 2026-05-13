using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Transform _visualRoot;

    private PlayerMove _playerMove;
    private int _lastFacingDirection = 1;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (_lastFacingDirection == _playerMove.FacingDirection)
        {
            return;
        }

        _lastFacingDirection = _playerMove.FacingDirection;

        Vector3 scale = _visualRoot.localScale;
        scale.x = _lastFacingDirection;
        _visualRoot.localScale = scale;
    }
}

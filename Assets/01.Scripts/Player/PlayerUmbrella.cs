using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerUmbrella : MonoBehaviour
{
    [SerializeField] private Transform _playerHandRoute;
    [SerializeField] private float _raiseAngle = -90f;
    [SerializeField] private float _lowerAngle = 0f;
    [SerializeField] private float _rotateDuration = 0.12f;

    private PlayerInput _playerInput;
    private Tween _rotateTween;
    private bool _wasUmbrellaHeld;

    public bool IsBlockingRain => _playerInput.IsUmbrellaHeld;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_wasUmbrellaHeld == _playerInput.IsUmbrellaHeld)
        {
            return;
        }

        _wasUmbrellaHeld = _playerInput.IsUmbrellaHeld;
        RotateUmbrella();
    }

    private void OnDestroy()
    {
        _rotateTween?.Kill();
    }

    private void RotateUmbrella()
    {
        float targetZ = _playerInput.IsUmbrellaHeld ? _raiseAngle : _lowerAngle;

        _rotateTween?.Kill();
        _rotateTween = _playerHandRoute.DOLocalRotate(new Vector3(0f, 0f, targetZ), _rotateDuration).SetEase(Ease.OutQuad);
    }

}

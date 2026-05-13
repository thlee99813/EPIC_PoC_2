using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerUmbrella : MonoBehaviour
{
    [SerializeField] private Transform _playerHandRoute;
    [SerializeField] private Transform _rainBlockDirection;

    [SerializeField] private float _raiseAngle = -90f;
    [SerializeField] private float _lowerAngle = 0f;
    [SerializeField] private float _rotateDuration = 0.12f;
    [SerializeField] private float _rainBlockAngle = 45f;


    private PlayerInput _playerInput;
    private Tween _rotateTween;
    private bool _wasUmbrellaHeld;
    private bool _isLocked;


    public bool IsBlockingRain
    {
        get
        {
            float angle = Vector2.Angle(_rainBlockDirection.up, Vector2.up);
            return angle <= _rainBlockAngle;
        }
    }

    public bool IsBlockingProjectile => _playerInput.IsUmbrellaHeld;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_isLocked)
        {
            return;
        }
        if (_wasUmbrellaHeld == _playerInput.IsUmbrellaHeld)
        {
            return;
        }

        _wasUmbrellaHeld = _playerInput.IsUmbrellaHeld;
        RotateUmbrella();
    }
    public void SetControlLocked(bool isLocked)
    {
        _isLocked = isLocked;

        if (_isLocked)
        {
            _rotateTween?.Kill();
        }
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

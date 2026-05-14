using Unity.Cinemachine;
using UnityEngine;

public class GlideCameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private CinemachinePositionComposer _positionComposer;
    [SerializeField] private PlayerGlide _playerGlide;
    [SerializeField] private float _defaultOrthographicSize = 4f;
    [SerializeField] private float _glideOrthographicSize = 5f;
    [SerializeField] private Vector3 _defaultFollowOffset = new Vector3(5f, 2f, -10f);
    [SerializeField] private float _zoomSpeed = 4f;


    private void LateUpdate()
    {
        float targetSize = _playerGlide.IsGliding ? _glideOrthographicSize : _defaultOrthographicSize;
        float sizeRatio = targetSize / _defaultOrthographicSize;

        Vector3 targetOffset = new Vector3(
            _defaultFollowOffset.x * sizeRatio,
            _defaultFollowOffset.y * sizeRatio,
            _defaultFollowOffset.z
        );

        LensSettings lens = _cinemachineCamera.Lens;
        lens.OrthographicSize = Mathf.Lerp(lens.OrthographicSize, targetSize, _zoomSpeed * Time.deltaTime);
        _cinemachineCamera.Lens = lens;

        _positionComposer.TargetOffset = Vector3.Lerp( _positionComposer.TargetOffset, new Vector3(targetOffset.x, targetOffset.y, 0f), _zoomSpeed * Time.deltaTime);

        _positionComposer.CameraDistance = Mathf.Abs(_defaultFollowOffset.z);
    }

}

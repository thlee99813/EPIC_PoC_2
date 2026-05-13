using Unity.Cinemachine;
using UnityEngine;

public class GlideCameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
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
  /*  Vector3 targetOffset = new Vector3(
            _defaultFollowOffset.x,
            _defaultFollowOffset.y,
            _defaultFollowOffset.z
        );
*/
    LensSettings lens = _cinemachineCamera.Lens;
    lens.OrthographicSize = Mathf.Lerp(lens.OrthographicSize, targetSize, _zoomSpeed * Time.deltaTime);
    _cinemachineCamera.Lens = lens;

    _cinemachineFollow.FollowOffset = Vector3.Lerp(_cinemachineFollow.FollowOffset, targetOffset, _zoomSpeed * Time.deltaTime);
}

}

using UnityEngine;

public class UmbrellaBlocker : MonoBehaviour
{
    [SerializeField] private PlayerUmbrella _playerUmbrella;

    public bool CanBlock => _playerUmbrella.IsBlockingProjectile;
}

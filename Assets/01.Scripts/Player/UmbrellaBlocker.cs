using UnityEngine;

public class UmbrellaBlocker : MonoBehaviour
{
    [SerializeField] private PlayerUmbrella _playerUmbrella;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private float _guardSPGain = 10f;

    public bool TryBlock()
    {
        if (!_playerUmbrella.IsBlockingProjectile)
        {
            return false;
        }

        _playerStats.GainGuardSP(_guardSPGain);
        return true;
    }
}

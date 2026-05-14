using UnityEngine;
using UnityEngine.UI;

public class PlayerSPUI : MonoBehaviour
{
    [SerializeField] private Image _rainSPFill;
    [SerializeField] private Image _guardSPFill;
    private PlayerStats _playerStats;

    private void OnDisable()
    {
        if (_playerStats != null)
        {
            _playerStats.SPChanged -= Refresh;
        }
    }

    public void Bind(PlayerStats playerStats)
    {
        if (_playerStats != null)
        {
            _playerStats.SPChanged -= Refresh;
        }

        _playerStats = playerStats;
        _playerStats.SPChanged += Refresh;

        

        Refresh(_playerStats.RainSP, _playerStats.GuardSP, _playerStats.MaxSP);
    }

    private void Refresh(float rainSP, float guardSP, float maxSP)
    {
        _rainSPFill.fillAmount = rainSP / maxSP;
        _guardSPFill.fillAmount = (rainSP + guardSP) / maxSP;
    }

}

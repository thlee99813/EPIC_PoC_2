using UnityEngine;

public class GameplayHUDController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [SerializeField] private PlayerSPUI _playerSPUI;

    private void Start()
    {
        _playerHealthUI.Bind(_player.Stats);
        _playerSPUI.Bind(_player.Stats);
    }
}

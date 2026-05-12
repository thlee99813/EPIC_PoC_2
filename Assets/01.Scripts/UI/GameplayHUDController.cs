using UnityEngine;

public class GameplayHUDController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    private void Start()
    {
        _playerHealthUI.Bind(_player.Health);
    }
}

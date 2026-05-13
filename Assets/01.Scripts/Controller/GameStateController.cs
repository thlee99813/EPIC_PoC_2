using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ResultHUDController _resultHUDController;

    public bool IsGameOver { get; private set; }

    private void Start()
    {
        _player.Stats.Died += SetGameOverState;
    }

    private void OnDestroy()
    {
        _player.Stats.Died -= SetGameOverState;
    }

    private void SetGameOverState()
    {
        IsGameOver = true;
        _resultHUDController.ShowGameOverImage();
        Time.timeScale = 0f;
    }
}

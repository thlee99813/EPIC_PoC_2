using UnityEngine;

public class ResultHUDController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverImage;

    private void Awake()
    {
        _gameOverImage.SetActive(false);
    }

    public void ShowGameOverImage()
    {
        _gameOverImage.SetActive(true);
    }
}

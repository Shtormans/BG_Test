using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _pausePanel;

    public static bool IsGamePaused { get; private set; }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsGamePaused = true;

        _pausePanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsGamePaused = true;

        _pausePanel.SetActive(false);
        _gamePanel.SetActive(true);
    }
}

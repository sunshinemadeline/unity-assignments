using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject MainMenuUI;
    public GameObject inGameUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject quitPopup;

    public bool isPaused = false;
    public bool isPlaying = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlayGame()
    {
        MainMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        gameOverUI.SetActive(false);

        Time.timeScale = 1f;
        isPlaying = true;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        isPlaying = false;
    }

    public void ShowQuitPopup()
    {
        quitPopup.SetActive(true);
    }

    public void HideQuitPopup()
    {
        quitPopup.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        PlayGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        isPlaying = false;

        pauseUI.SetActive(false);
        inGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


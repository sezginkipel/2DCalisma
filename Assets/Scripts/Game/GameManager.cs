using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gamePausedPanel;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public GameObject deathPanel;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Ensure panels are off at the start
        if (gamePausedPanel != null) gamePausedPanel.SetActive(false);
        if (deathPanel != null) deathPanel.SetActive(false);

        ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                if (UIManager.Instance != null) UIManager.Instance.ShowMarketPanel(false);
                break;
            case GameState.Paused: // For Market
                Time.timeScale = 0f;
                if (UIManager.Instance != null) UIManager.Instance.ShowMarketPanel(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                // Assuming UIManager will also handle the death panel
                // if (UIManager.Instance != null) UIManager.Instance.ShowDeathPanel(true);
                if (deathPanel != null) deathPanel.SetActive(true); // Keeping this for now
                break;
        }
    }

    public void RestartLevel()
    {
        // Time.timeScale is set to 1 in ChangeState, but good to be sure
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

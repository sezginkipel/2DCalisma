using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject marketPanel;
    // Add other panels like deathPanel, pausePanel etc. here

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
        // Ensure all panels are off at the start
        if (marketPanel != null) marketPanel.SetActive(false);
    }

    public void ShowMarketPanel(bool show)
    {
        if (marketPanel != null) marketPanel.SetActive(show);
    }

    public void CloseMarket()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        }
    }
}

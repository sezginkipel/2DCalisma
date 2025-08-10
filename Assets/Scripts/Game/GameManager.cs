using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game states
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }


    public GameObject gamePausedPanel;


    private GameState currentState;

    private void Start()
    {
        currentState = GameState.Playing;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                // Game is currently being played
                break;
            case GameState.Paused:
                // Game is paused
                break;
            case GameState.GameOver:
                // Game is over
                break;
        }
    }
}

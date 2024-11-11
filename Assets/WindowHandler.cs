using UnityEngine;

public class WindowHandler
{
    public void WindowToggle(GameObject gameObject, GameState currentGameState, GameState gameStateToChangeTo)
    {
        if (GameStateManager.CurrentGameState == currentGameState)
        {
            WindowOpen(gameObject, gameStateToChangeTo);
        }
        else if (GameStateManager.CurrentGameState == gameStateToChangeTo)
        {
            WindowClose(gameObject, currentGameState);
        }
    }

    public void WindowOpen(GameObject gameObject, GameState gameStateToChangeTo)
    {
        gameObject.SetActive(true);
        GameStateManager.ChangeGameState(gameStateToChangeTo);
        Time.timeScale = 0f;
    }

    public void WindowClose(GameObject gameObject, GameState currentGameState)
    {
        gameObject.SetActive(false);
        GameStateManager.ChangeGameState(currentGameState);
        Time.timeScale = 1f;
    }
}

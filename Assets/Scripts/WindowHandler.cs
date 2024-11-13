using System.Collections.Generic;
using UnityEngine;

public class WindowHandler
{
    public void WindowToggle(List<GameObject> gameObjects, GameState currentGameState, GameState gameStateToChangeTo)
    {
        if (GameStateManager.CurrentGameState == currentGameState)
        {
            WindowOpen(gameObjects, gameStateToChangeTo);
        }
        else if (GameStateManager.CurrentGameState == gameStateToChangeTo)
        {
            WindowClose(gameObjects, currentGameState);
        }
    }
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

    public void WindowOpen(List<GameObject> gameObjects, GameState gameStateToChangeTo)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
        GameStateManager.ChangeGameState(gameStateToChangeTo);
        Time.timeScale = 0f;
    }
    public void WindowOpen(GameObject gameObject, GameState gameStateToChangeTo)
    {
        gameObject.SetActive(true);
        GameStateManager.ChangeGameState(gameStateToChangeTo);
        Time.timeScale = 0f;
    }

    public void WindowClose(List<GameObject> gameObjects, GameState currentGameState)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
        GameStateManager.ChangeGameState(currentGameState);
        Time.timeScale = 1f;
    }    
    public void WindowClose(GameObject gameObject, GameState currentGameState)
    {
        gameObject.SetActive(false);
        GameStateManager.ChangeGameState(currentGameState);
        Time.timeScale = 1f;
    }
}

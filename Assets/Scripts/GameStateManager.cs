using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public static GameState CurrentGameState { get; private set; }
    public static GameState LastGameState { get; private set; }

    static GameStateManager()
    {
        CurrentGameState = GameState.InGame;
        LastGameState = GameState.None;
    }
    
    public static void ChangeGameState(GameState newGameState)
    {
        LastGameState = CurrentGameState;
        CurrentGameState = newGameState;
    }
}

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : Interaction, IInputHandler
{
    [TabGroup("Minigame")]
    [SerializeField] private MinigameType minigameType;
    [TabGroup("Minigame")] 
    public bool canSkipTime;
    
    public override void Interact()
    {
        if (!enabled) return;
        
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        EventsManager.InvokeHideObjectText();
        SceneManager.LoadScene(minigameType.ToString(), LoadSceneMode.Additive);
        GameStateManager.ChangeGameState(GameState.Minigame);
        InputManager.Instance.RegisterHandler(this);
    }

    public void HandleInput()
    {
        if (!enabled) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(minigameType.ToString());
            GameStateManager.ChangeGameState(GameState.InGame);

            if (canSkipTime)
            {
                EventsManager.InvokeChangeTimeEvent();
            }
            
            InputManager.Instance.UnregisterHandler(this);
        }
    }
}


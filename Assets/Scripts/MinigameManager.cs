using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : Interaction, IInputHandler
{
    [TabGroup("ImageReference")]
    [SerializeField] private GameObject rawImage;
    [TabGroup("Minigame")]
    [SerializeField] private MinigameType minigameType;
    [TabGroup("Minigame")] 
    public bool canSkipTime;
    
    private void Start()
    {
        rawImage.SetActive(false);
    }
    
    public override void Interact()
    {
        if (!enabled) return;
        
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        rawImage.SetActive(true);
        SceneManager.LoadScene(minigameType.ToString(), LoadSceneMode.Additive);
        GameStateManager.ChangeGameState(GameState.Minigame);
        InputManager.Instance.RegisterHandler(this);
    }

    public void HandleInput()
    {
        if (!enabled) return;
        
        if (Input.GetKeyDown(KeyCode.Escape) && rawImage.activeInHierarchy)
        {
            rawImage.SetActive(false);
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


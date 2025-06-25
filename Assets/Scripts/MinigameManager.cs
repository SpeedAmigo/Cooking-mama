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
    [TabGroup("Minigame")] 
    public bool canBePlayedAgain;
    [TabGroup("Minigame")] 
    public bool loadDayInt;
    [TabGroup("Dependencies")] 
    public DayNightScript dayNightScript;

    private string uniqueID;
    [SerializeField] private int lastPlayedOnDay;
    
    private void Awake()
    {
        base.Awake();
        
        uniqueID = gameObject.name;

        if (ES3.KeyExists("LastPlayed_" + uniqueID) && loadDayInt)
        {
            lastPlayedOnDay = ES3.Load<int>("LastPlayed_" + uniqueID);
        }
    }
    
    public override void Interact()
    {
        if (!enabled) return;
        if (dayNightScript.CurrentDayCycle == DayCycles.Night)
        {
            Debug.Log("I should go to sleep");
            return;
        }

        if (lastPlayedOnDay == dayNightScript.GetDayCount() && !canBePlayedAgain)
        {
            return;
        }
        
        if (GameStateManager.CurrentGameState != GameState.InGame) return;

        lastPlayedOnDay = dayNightScript.GetDayCount();
        //EventsManager.InvokeHideObjectText();
        EventsManager.InvokeClearObjectText();
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
            
            SaveLastPlayedDay();
            InputManager.Instance.UnregisterHandler(this);
        }
    }

    private void SaveLastPlayedDay()
    {
        ES3.Save("LastPlayed_" + uniqueID, lastPlayedOnDay);
    }
}


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
    [TabGroup("Minigame")] 
    [SerializeField] private SoObjectText[] soObjectTexts; 
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
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        if (!enabled) return;
        if (dayNightScript.CurrentDayCycle == DayCycles.Night)
        {
            if (soObjectTexts.Length > 0)
            {
                soObjectTexts[0].ShowText(soObjectTexts[0].popUpText);
            }
            return;
        }

        if (lastPlayedOnDay == dayNightScript.GetDayCount() && !canBePlayedAgain)
        {
            if (soObjectTexts.Length >= 1)
            {
                soObjectTexts[1].ShowText(soObjectTexts[1].popUpText);
            }
            
            return;
        }
        
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


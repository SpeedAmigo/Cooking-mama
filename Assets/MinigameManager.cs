using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : Interaction
{
    [SerializeField] private GameObject rawImage;
    [SerializeField] private MinigameType minigameType;

    private void Start()
    {
        rawImage.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && rawImage.activeInHierarchy)
        {
            rawImage.SetActive(false);
            SceneManager.UnloadSceneAsync(minigameType.ToString());
            GameStateManager.ChangeGameState(GameState.InGame);
        }
    }

    public override void Interact()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        rawImage.SetActive(true);
        SceneManager.LoadScene(minigameType.ToString(), LoadSceneMode.Additive);
        GameStateManager.ChangeGameState(GameState.Minigame);
    }
}


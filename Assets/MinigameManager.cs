using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : Interaction
{
    [SerializeField] private GameObject image;
    [SerializeField] private MinigameType minigameType;

    private void Start()
    {
        image.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && image.activeInHierarchy)
        {
            image.SetActive(false);
            SceneManager.UnloadSceneAsync(minigameType.ToString());
            GameStateManager.ChangeGameState(GameState.InGame);
        }
    }

    public override void Interact()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        image.SetActive(true);
        SceneManager.LoadScene(minigameType.ToString(), LoadSceneMode.Additive);
        GameStateManager.ChangeGameState(GameState.Minigame);
    }
}

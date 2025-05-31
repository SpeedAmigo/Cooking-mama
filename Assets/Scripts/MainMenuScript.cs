using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MainMenuScript : MonoBehaviour
{
    public void StartMethod()
    {
        SceneLoader.LoadScene(1);
        GameStateManager.ChangeGameState(GameState.InGame);
    }

    public void ExitMethod()
    {
        Application.Quit();
    }
}

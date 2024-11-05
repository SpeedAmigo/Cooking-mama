using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    private void PauseToggle()
    {
        if (GameStateManager.CurrentGameState == GameState.InGame)
        {
            ActivatePauseMenu();
        }
        else if (GameStateManager.CurrentGameState == GameState.PauseMenu)
        {
            DeactivatePauseMenu();
        }
    }

    private void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        GameStateManager.ChangeGameState(GameState.PauseMenu);
        Time.timeScale = 0f;
    }

    private void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameStateManager.ChangeGameState(GameState.InGame);
        Time.timeScale = 1f;
    }

    public void ResumeMethod()
    {
        DeactivatePauseMenu();
    }

    public void MainMenuMethod()
    {
        SceneLoader.LoadScene(0);
        pauseMenu.SetActive(false);
        GameStateManager.ChangeGameState(GameState.MainMenu);
        Time.timeScale = 1f;
    }

    public void ExitGameMethod()
    {
        Application.Quit();
    }
    
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }
}

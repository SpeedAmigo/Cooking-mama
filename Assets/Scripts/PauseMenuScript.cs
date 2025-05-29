using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    private WindowHandler _windowHandler = new();

    public void ResumeMethod()
    {
        _windowHandler.WindowClose(pauseMenu, GameState.InGame);
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

    public void TogglePauseMenu()
    {
        _windowHandler.WindowToggle(pauseMenu, GameState.InGame, GameState.PauseMenu);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }

    private void Update()
    {
        
    }
}

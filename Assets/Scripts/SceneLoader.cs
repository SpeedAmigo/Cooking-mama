using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(int sceneID)
    {
        LoadSceneAsync(sceneID);
    }
    
    public static void LoadScene(string sceneName)
    {
        LoadSceneAsync(sceneName);
    }
    
    private static async void LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (operation != null && !operation.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
    }    
    private static async void LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (operation != null && !operation.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
    }
}

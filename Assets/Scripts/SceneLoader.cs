using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(int sceneID)
    {
        LoadSceneAsync(sceneID);
    }
    
    private static async void LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (operation != null && !operation.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
    }
}

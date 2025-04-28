using UnityEngine;
using UnityEngine.UI;

public class SceneEssentialsManager : MonoBehaviour
{
    public static SceneEssentialsManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

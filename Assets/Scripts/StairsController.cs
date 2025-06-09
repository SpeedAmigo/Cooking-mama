using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairsController : MonoBehaviour
{
    [TabGroup("Dependencies")]
    [SerializeField] private Image transitionImage;
    [TabGroup("Dependencies")]
    [SerializeField] private DayNightScript dayNightScript;
    [TabGroup("Dependencies")]
    [SerializeField] private SO_TransitionProperties properties;
    [TabGroup("Properties")]
    [SceneDropdown] [SerializeField] private string sceneName;
    [TabGroup("Properties")]
    [SerializeField] [MinValue(1)] private int spawnPointIndex;
    [TabGroup("Properties")]
    [SerializeField] private float unlockOnDay;
    
    
    
    private bool canTransition = true;
    public bool CanTransition { set {canTransition = value; }}

    private void Start()
    {
        transitionImage.material.SetFloat("_Progress", 0);
        transitionImage.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            if (!canTransition) return;
            if (unlockOnDay > dayNightScript.GetDayCount()) return;
            
            TransitionManager.IsTransitioning = true;
            GameStateManager.ChangeGameState(GameState.SceneLoading);
            StartCoroutine(FadeIn(sceneName, properties.transitionDuration, spawnPointIndex));
        }
    }

    private IEnumerator FadeIn(string sceneName, float duration, int spawnPointIndex) 
    {
        float elapsedTime = 0;
        transitionImage.gameObject.SetActive(true);
        transitionImage.material.SetFloat("_Progress", 0);
        
        TransitionManager.SpawnPointIndex = spawnPointIndex;

       AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
       asyncLoad.allowSceneActivation = false;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transitionImage.material.SetFloat("_Progress", Mathf.Clamp01(elapsedTime / duration));
            yield return null;
        }

       if (asyncLoad.progress >= 0.9f)
       { 
           yield return null;
       }
        
        asyncLoad.allowSceneActivation = true;
    }
}

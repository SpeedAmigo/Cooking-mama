using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private SO_TransitionProperties properties;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerScript player;
    [SceneDropdown] [SerializeField] private string sceneName;
    
    private bool canTransition = true;
    public bool CanTransition { set {canTransition = value; }}

    private void Start()
    {
        transitionImage.material.SetFloat("_Progress", 0);
        transitionImage.gameObject.SetActive(false);
        MoveToSpawnPoint();
    }

    private void MoveToSpawnPoint()
    {
        if (TransitionManager.IsTransitioning)
        {
            player.transform.position = spawnPoint.position;
            TransitionManager.IsTransitioning = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            if (!canTransition) return;
            
            TransitionManager.IsTransitioning = true;
            GameStateManager.ChangeGameState(GameState.SceneLoading);
            StartCoroutine(FadeIn(sceneName, properties.transitionDuration));
        }
    }

    private IEnumerator FadeIn(string sceneName, float duration) 
    {
        float elapsedTime = 0;
        transitionImage.gameObject.SetActive(true);
        transitionImage.material.SetFloat("_Progress", 0);

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

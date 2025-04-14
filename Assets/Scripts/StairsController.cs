using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private Image transitionImage;
    [SerializeField] private SO_TransitionProperties properties;
    [SceneDropdown] [SerializeField] private string sceneName;
    

    private void Start()
    {
        transitionImage.material.SetFloat("_Progress", 0);
        transitionImage.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
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

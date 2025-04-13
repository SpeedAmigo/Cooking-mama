using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private Image transitionImage;
    [SceneDropdown] [SerializeField] private string sceneName;
    
    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        transitionImage.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            StartCoroutine(FadeIn(sceneName, fadeDuration));
        }
    }

    private IEnumerator FadeIn(string sceneName, float duration) 
    {
        float elapsedTime = 0;
        transitionImage.gameObject.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            transitionImage.material.SetFloat("_Progress", 0 + progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (asyncLoad.progress >= 0.9f)
        {
            yield return null;
        }
        
        asyncLoad.allowSceneActivation = true;
        //transitionImage.material.SetFloat("_Progress", 0);
        //transitionImage.gameObject.SetActive(false);
    }
}

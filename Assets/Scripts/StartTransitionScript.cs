using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartTransitionScript : MonoBehaviour
{
    [SerializeField] private SO_TransitionProperties properties;
    [SerializeField] private Image transitionImage;
    
    private void Start()
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.material.SetFloat("_Progress", 1);
        StartCoroutine(FadeOut(properties.transitionDuration));
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            transitionImage.material.SetFloat("_Progress", 1 - progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transitionImage.material.SetFloat("_Progress", 0);
        transitionImage.gameObject.SetActive(false);
    }
}

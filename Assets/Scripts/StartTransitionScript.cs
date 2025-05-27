using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StartTransitionScript : SerializedMonoBehaviour
{
    [SerializeField] private SO_TransitionProperties properties;
    [SerializeField] private Image transitionImage;
    [SerializeField] private PlayerScript player;
    
    [SerializeField]
    [DictionaryDrawerSettings(KeyLabel = "Index", ValueLabel = "Spawn Point")]
    private Dictionary<int, Transform> spawnPoints = new();
    
    private void Start()
    {
        GameStateManager.ChangeGameState(GameState.InGame);
        transitionImage.gameObject.SetActive(true);
        transitionImage.material.SetFloat("_Progress", 1);
        MoveToSpawnPoint(TransitionManager.SpawnPointIndex);
        StartCoroutine(FadeOut(properties.transitionDuration));
    }
    private void MoveToSpawnPoint(int spawnPointIndex)
    {
        if (!TransitionManager.IsTransitioning) return;

        if (!spawnPoints.ContainsKey(spawnPointIndex))
        {
            player.transform.position = transform.position;
        }
        
        Transform targetPosition = spawnPoints[spawnPointIndex];
        player.transform.position = targetPosition.position;
        
        TransitionManager.IsTransitioning = false;
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

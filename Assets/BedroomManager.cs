using UnityEngine;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] private Sprite cleanBedSprite;
    [SerializeField] private Sprite messyBedSprite;
    
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject[] paperTrashes;

    private void Awake()
    {
        EventsManager.BedroomMinigameCompleteEvent += MinigameComplete;
    }

    private void OnDisable()
    {
        EventsManager.BedroomMinigameCompleteEvent -= MinigameComplete;
    }

    private void MinigameComplete()
    {
        bed.GetComponent<SpriteRenderer>().sprite = cleanBedSprite;
        
        foreach (GameObject paperTrash in paperTrashes)
        {
            paperTrash.gameObject.SetActive(false);
        }
    }
}

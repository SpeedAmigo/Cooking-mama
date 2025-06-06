using UnityEngine;

public class BedroomMinigameManager : MonoBehaviour
{
    [SerializeField] private BrushScript brush;
    
    private int trashInt;
    private int webInt;
    private bool bedComplete;
    private bool minigameComplete = false;
    
    public Transform binTransform;
    public bool holdingBrush;

    private void Start()
    {
        EventsManager.InvokeOnGetBedroomManager(this);
    }
    
    public void ActiveBrush()
    {
        brush.gameObject.SetActive(true);
        brush.BrushAnimation();
        EventsManager.InvokeOnGetBedroomManager(this);
    }

    public void TrashComplete()
    {
        trashInt ++;
    }

    public void WebComplete()
    {
        webInt ++;
    }

    public void BedComplete()
    {
        bedComplete = true;
    }

    private void Update()
    {
        if (!minigameComplete && trashInt == 4 && webInt == 4 && bedComplete)
        {
            minigameComplete = true;
            EventsManager.InvokeBedroomMinigameCompleteEvent();
        }
    }
}

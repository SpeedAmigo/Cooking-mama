using UnityEngine;
using System.Collections.Generic;

public class BedroomMinigameManager : MonoBehaviour
{
    [SerializeField] private BrushScript brush;
    
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
}

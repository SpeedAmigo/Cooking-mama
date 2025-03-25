using UnityEngine;
using System.Collections.Generic;

public class BedroomMinigameManager : MonoBehaviour
{
    [SerializeField] private BrushScript brush;
    
    public Transform binTransform;
    public List<TrashScript> trashes;
    public List<WebScript> webs;
    
    public bool holdingBrush;

    private void Start()
    {
        EventsManager.OnDrawerOpenEvent += ActiveBrush;

        foreach (var trash in trashes)
        {
            trash.binTransform = binTransform;
        }

        foreach (var web in webs)
        {
            web.bedroomManager = this;
        }
    }

    private void Update()
    {
        holdingBrush = brush.GetBrushState();
    }

    private void ActiveBrush()
    {
        brush.gameObject.SetActive(true);
        brush.BrushAnimation();
    }
}

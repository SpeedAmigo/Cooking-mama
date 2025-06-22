using UnityEngine;
using UnityEngine.EventSystems;

public class SkinPointScript : MinigameAbstract
{
    [SerializeField] private PointsContainerScript container;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        container.AddToPoints(this);
        gameObject.SetActive(false);
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

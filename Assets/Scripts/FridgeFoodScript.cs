using UnityEngine;
using UnityEngine.EventSystems;

public class FridgeFoodScript : MinigameAbstract
{
    [SerializeField] private GameObject objectToActivate;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        objectToActivate.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnPointerUp(PointerEventData eventData) { }
}

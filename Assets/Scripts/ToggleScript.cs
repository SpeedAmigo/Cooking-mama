using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ToggleScript : MinigameAbstract
{
    public UnityEvent OnToggle;
    
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnToggle?.Invoke();
    }
}

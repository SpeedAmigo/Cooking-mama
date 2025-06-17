using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MinigameAbstract : 
    MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IPointerDownHandler, 
    IPointerUpHandler, 
    IDragHandler, 
    IPointerClickHandler
{
    private OutlineFx.OutlineFx outline;
    
    protected virtual void Awake()
    {
        outline = GetComponent<OutlineFx.OutlineFx>() ?? gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = false;
    }
    
    protected virtual Vector3 GetWorldPosition(Camera camera)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return camera.ScreenToWorldPoint(mouseScreenPos);
    }

    public abstract void OnPointerDown(PointerEventData eventData);
    public abstract void OnDrag(PointerEventData eventData);
    public abstract void OnPointerUp(PointerEventData eventData);
    public abstract void OnPointerClick(PointerEventData eventData);
}
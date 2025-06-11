using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CounterFood : MinigameAbstract
{
    public bool continousUpdate = false;
    
    private bool isHeld = false;
    
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        renderer.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        if (continousUpdate) return;
        
        gameObject.transform.position = GetWorldPosition(Camera.main);
        Debug.Log("OnDrag");
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        renderer.sortingOrder--;
    }
    
    private void LateUpdate()
    {
        if (continousUpdate && isHeld)
        {
            gameObject.transform.position = GetWorldPosition(Camera.main);
            Debug.Log("OnLateUpdate");
        }
    }
    
    public override void OnPointerClick(PointerEventData eventData) { }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookScript : BooksAbstractMinigame
{
    public bool isDragging;
    public ShelfManager shelfManager;

    private float startZAxis;

    private void Start()
    {
        startZAxis = transform.position.z;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        
        var position = transform.position;
        position.z = 11f;
        transform.position = position;
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = new Vector3(GetWorldPosition().x, transform.position.y, transform.position.z);
            
            shelfManager.UpdateBookOrder();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        shelfManager.UpdateBookOrder();
        
        var position = transform.position;
        position.z = startZAxis;
        transform.position = position;
    }

    public override void OnPointerClick(PointerEventData eventData) { }
    
    private Vector3 GetWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}

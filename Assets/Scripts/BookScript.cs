using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookScript : BooksAbstractMinigame
{
    public bool isDragging;
    public ShelfManager shelfManager;

    private float startZAxis;
    private Renderer renderer;

    private void Start()
    {
        startZAxis = transform.position.z;
        renderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        renderer.sortingOrder = 1;
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
        renderer.sortingOrder = 0;
        shelfManager.UpdateBookOrder();
        shelfManager.IsCorrectOrder();
    }

    public override void OnPointerClick(PointerEventData eventData) { }
    
    private Vector3 GetWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}

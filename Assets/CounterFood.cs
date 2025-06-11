using System;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;

public class CounterFood : MinigameAbstract
{
    public bool continousUpdate = false;
    public bool useCuttingBoard = true;
    
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
        if (useCuttingBoard)
        {
            CheckForCuttingBoard();
        }
        
        isHeld = false;
        renderer.sortingOrder--;
    }

    private void CheckForCuttingBoard()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, Vector2.zero, 0.1f);

        Debug.DrawRay(transform.position, Vector2.zero, Color.red);
        
        if (hit.collider.TryGetComponent<ChoppingBoardScript>(out var board))
        {
            transform.localPosition = board.transform.position;
            Debug.Log("Board");
        }
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

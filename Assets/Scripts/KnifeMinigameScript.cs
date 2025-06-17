using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Serialization;

public class KnifeMinigameScript : MinigameAbstract
{
    public KitchenGameManager manager;
    
    private bool isHeld = false;
    private Renderer rend;
    private Vector2 startPos;

    [SerializeField] private ChoppingBoardScript board;
    
    [SerializeField] private float moveMagnitude = 1f;
    [SerializeField] private Vector2 lastPos;
    [SerializeField] private Vector2 currentPos;
    [SerializeField] private Vector2 deltaPos;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        rend.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        gameObject.transform.position = GetWorldPosition(manager.MinigameCamera);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        rend.sortingOrder--;
        gameObject.transform.DOMove(startPos, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out ChoppingBoardScript script)) return;
        board = script;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out ChoppingBoardScript script)) return;
        board = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (board == null) return;
        
        currentPos = transform.position;
        deltaPos = currentPos - lastPos;
        
        if (deltaPos.magnitude < moveMagnitude) return;
        
        lastPos = currentPos;
        
        if (board.currentFood == null) return;        

        if (board.currentFood.CurrentCuts < board.currentFood.RequiredCuts)
        {
            board.currentFood.CurrentCuts++;
        }
        else if (board.currentFood.CurrentCuts >= board.currentFood.RequiredCuts)
        {
            board.SpawnCuttedFood();
        }
    }

    public override void OnPointerClick(PointerEventData eventData) { }
}

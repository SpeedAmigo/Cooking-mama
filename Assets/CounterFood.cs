using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class CounterFood : MinigameAbstract
{ 
    public KitchenGameManager manager;
    
    [TabGroup("CounterFood")]
    [SerializeField] protected bool continousUpdate = false;
    [TabGroup("CounterFood")]
    [SerializeField] protected bool useBoard = true;
    [TabGroup("CounterFood")]
    [SerializeField] [ReadOnly] protected bool onBoard;
    [TabGroup("CounterFood")]
    [SerializeField] [ReadOnly] protected Vector2 lastSafePosition;
    
    protected bool isHeld = false;
    protected SpriteRenderer renderer;
    
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        lastSafePosition = transform.position;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        lastSafePosition = transform.position;
        isHeld = true;
        renderer.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        if (continousUpdate) return;
        
        gameObject.transform.position = GetWorldPosition(Camera.main);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        renderer.sortingOrder--;

        bool isSafe = manager.CheckIfSafe(transform.position);
        
        if (!isSafe)
        {
            transform.DOMove(lastSafePosition, 0.5f);
            return;
        } 
        lastSafePosition = transform.position;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent<ChoppingBoardScript>(out var board)) return;

        if (!isHeld && useBoard)
        {
            gameObject.transform.position = board.transform.position;
            onBoard = true;
        }
        else
        {
            onBoard = false;
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
    
    public override void OnPointerClick(PointerEventData eventData) {}
}

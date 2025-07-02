using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BathroomItem : MinigameAbstract
{
    [SerializeField] private int itemIndex;
    [SerializeField] private MaskType maskType;
    
    private Vector3 startPosition;
    private SpriteRenderer renderer;
    private bool isHeld;
    
    [SerializeField] private SophieScript sophieScript;

    private void Start()
    {
        startPosition = transform.position;
        renderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        renderer.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;

        transform.position = GetWorldPosition(BathroomManager.Instance.mainCamera);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (sophieScript != null)
        {
            sophieScript.PutItemOnFace(itemIndex, maskType);
            Debug.Log("dziala");
        }
        
        isHeld = false;
        renderer.sortingOrder--;
        transform.DOMove(startPosition, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out SophieScript sophieScript)) return;
        this.sophieScript = sophieScript;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out SophieScript sophieScript)) return;
        this.sophieScript = null;
    }
    
    public override void OnPointerClick(PointerEventData eventData) { }
}

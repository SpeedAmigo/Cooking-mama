using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowelScript : MinigameAbstract
{
    private Vector3 startPosition;
    private bool isHeld;
    private SpriteRenderer renderer;

    [SerializeField] SophieScript sophieScript;
    
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite secondSprite;

    [SerializeField] private float wipingMagnitude;
    [SerializeField] private float moveMagnitude = 1f;
    [SerializeField] private Vector2 lastPos;
    [SerializeField] private Vector2 currentPos;
    [SerializeField] private Vector2 deltaPos;

    private void Start()
    {
        startPosition = transform.position;
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out SophieScript sophie)) return;
        sophieScript = sophie;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (sophieScript == null || !sophieScript.needToWipe) return;
        
        currentPos = transform.position;
        deltaPos = currentPos - lastPos;
        
        if (deltaPos.magnitude < moveMagnitude) return;
        
        lastPos = currentPos;
        
        sophieScript.FadeOutBubble(wipingMagnitude);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out SophieScript sophie)) return;
        sophieScript = null;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        renderer.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        
        renderer.sprite = secondSprite;
        transform.position = GetWorldPosition(BathroomManager.Instance.mainCamera);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        renderer.sortingOrder--;
        transform.DOMove(startPosition, 0.5f).OnComplete(() => renderer.sprite = baseSprite);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

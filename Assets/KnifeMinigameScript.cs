using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class KnifeMinigameScript : MinigameAbstract
{
    private bool isHeld = false;
    private Renderer rend;
    private Vector2 startPos;

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
        gameObject.transform.position = GetWorldPosition(Camera.main);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        rend.sortingOrder--;
        gameObject.transform.DOMove(startPos, 0.5f);
    }

    public override void OnPointerClick(PointerEventData eventData) { }
}

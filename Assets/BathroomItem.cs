using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BathroomItem : MinigameAbstract
{
    private Vector3 startPosition;
    private SpriteRenderer renderer;
    private bool isHeld;

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
        isHeld = false;
        renderer.sortingOrder--;
        transform.DOMove(startPosition, 0.5f);
    }

    public override void OnPointerClick(PointerEventData eventData) { }
}

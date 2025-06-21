using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SophieScript : MinigameAbstract
{
    private Animator animator;

    public int currentItemIndex;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("Bend");
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

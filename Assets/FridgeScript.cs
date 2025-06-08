using UnityEngine;
using UnityEngine.EventSystems;

public class FridgeScript : MinigameAbstract
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnPointerUp(PointerEventData eventData) { }

    public override void OnPointerClick(PointerEventData eventData)
    {
        bool open = animator.GetBool("Open");
        animator.SetBool("Open", !open);
    }
}

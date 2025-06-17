using UnityEngine;
using UnityEngine.EventSystems;

public class FridgeScript : MinigameAbstract
{
    private Animator animator;

    [SerializeField] private GameObject fridgeFood;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        fridgeFood.SetActive(false);
    }

    public void SetFridgeFoodActive(int active)
    {
        fridgeFood.SetActive(active == 1);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        bool open = animator.GetBool("Open");
        animator.SetBool("Open", !open);
    }

    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnPointerUp(PointerEventData eventData) { }
}

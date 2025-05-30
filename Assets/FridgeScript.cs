using UnityEngine;

public class FridgeScript : KitchenMinigameAbstract
{
    private Animator animator;
    
    protected override void OnRaycastClick()
    {
        bool open = animator.GetBool("Open");
        animator.SetBool("Open", !open);
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}

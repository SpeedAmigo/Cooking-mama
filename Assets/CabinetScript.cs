using UnityEngine;

public class CabinetScript : BedroomCleanAbstract
{
    private Animator animator;

    protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    
    protected override void OnMouseDown()
    {
        bool drawerState = animator.GetBool("drawerOpen");
        
        animator.SetBool("drawerOpen", !drawerState);
    }
}

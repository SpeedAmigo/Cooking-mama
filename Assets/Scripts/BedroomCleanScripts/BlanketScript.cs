using UnityEngine;

public class BlanketScript : BedroomCleanAbstract
{
    private Animator animator;
    private bool bedCleaned;
    
    public MinigameMouseScrenToWorld mouseScrenToWorld;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    protected override void OnRaycastClick()
    {
        animator.SetBool("cleanBed", true);
        bedCleaned = true;
    }

    public override void OnRaycastEnter()
    {
        if (!bedCleaned)
        {
            base.OnRaycastEnter();
        }
    }
}

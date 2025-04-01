using UnityEngine;

public class BlanketScript : BedroomCleanAbstract
{
    private Animator animator;
    private bool bedCleaned;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    protected override void OnRaycastClick()
    {
        animator.SetBool("cleanBed", true);
        bedCleaned = true;
        manager.BedComplete();
    }

    public override void OnRaycastEnter()
    {
        if (!bedCleaned)
        {
            base.OnRaycastEnter();
        }
    }
}

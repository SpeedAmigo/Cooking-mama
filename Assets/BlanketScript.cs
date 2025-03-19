using UnityEngine;

public class BlanketScript : BedroomCleanAbstract
{
    private Animator animator;
    private bool bedCleaned;
    
    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void OnMouseDown()
    {
        animator.SetBool("cleanBed", true);
        bedCleaned = true;
    }

    protected override void OnMouseEnter()
    {
        if (!bedCleaned)
        {
            base.OnMouseEnter();
        }
    }
}

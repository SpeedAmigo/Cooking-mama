using UnityEngine;
using System;

public class CabinetScript : BedroomCleanAbstract
{
    private Animator animator;
    private bool hasOpenedOnce;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    protected override void OnMouseDown()
    {
        bool drawerState = animator.GetBool("drawerOpen");
        
        animator.SetBool("drawerOpen", !drawerState);
    }

    protected void OnDrawerOpen()
    {
        if (!hasOpenedOnce)
        {
            manager.ActiveBrush();
            hasOpenedOnce = true;
        }
    }
}

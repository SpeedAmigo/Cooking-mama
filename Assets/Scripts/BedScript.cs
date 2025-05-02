using System;
using UnityEngine;

public class BedScript : Interaction
{
    private MinigameManager manager;

    private void Start()
    {
        manager.GetComponent<MinigameManager>();
    }

    public override void Interact()
    {
        if (manager.enabled) return;
        
        Debug.Log("Dziala");
    }
}

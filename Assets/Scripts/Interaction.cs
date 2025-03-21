using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interaction : MonoBehaviour, IInteractable
{
    protected virtual void Awake()
    {
        SetOutline();
    }

    private void SetOutline()
    {
        var outline = gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    public abstract void Interact();
}
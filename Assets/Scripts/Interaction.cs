using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interaction : MonoBehaviour, IInteractable
{
    public string newName;
    public string description;

    protected virtual void Start()
    {
        var outline = gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    public abstract void Interact();
}
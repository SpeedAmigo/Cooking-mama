using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Interaction : MonoBehaviour, IInteractAble
{
    public string newName;
    public string description;
    
    public abstract void Interact();
}
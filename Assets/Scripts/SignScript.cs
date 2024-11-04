using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : Interaction
{
    [SerializeField] private signType _signType;

    private string SignType()
    {
        switch (_signType)
        {
            case signType.East:
                description = "East";
                break;
            case signType.West:
                description = "West";
                break;
        }
        return description;
    }
    
    public override void Interact()
    {
        Debug.Log($"{newName} is pointing to the {SignType()}");
    }
}

public enum signType
{
    East,
    West
}

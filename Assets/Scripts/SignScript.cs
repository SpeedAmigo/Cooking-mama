using UnityEngine;

public class SignScript : Interaction
{
    [SerializeField] private SignType _signType;
    [SerializeField] private string description;

    private string SignType()
    {
        switch (_signType)
        {
            case global::SignType.East:
                description = "East";
                break;
            case global::SignType.West:
                description = "West";
                break;
        }
        return description;
    }
    
    public override void Interact()
    {
        Debug.Log($"sign is pointing to the {SignType()}");
    }
}

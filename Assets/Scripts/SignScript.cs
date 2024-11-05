using UnityEngine;

public class SignScript : Interaction
{
    [SerializeField] private SignType _signType;
    
    public void Start()
    {
        base.Start();
    }
    
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
        Debug.Log($"{newName} is pointing to the {SignType()}");
    }
}

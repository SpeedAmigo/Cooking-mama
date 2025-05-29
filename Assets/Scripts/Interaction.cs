using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteractable
{
    [TabGroup("PopUpText")]
    [HideLabel]
    [SerializeField] protected SoObjectText objectText;
    
    protected virtual void Awake()
    {
        SetOutline();
    }
    
    private void SetOutline()
    {
        var outline = gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    // method called by clickin mouse on object
    public void TextTrigger()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        if (objectText != null)
        {
            objectText.ShowText(objectText.popUpText);
        }
    }
    
    public abstract void Interact();
}
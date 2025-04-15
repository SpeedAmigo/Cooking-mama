using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteractable
{
    [SerializeField] protected string popUpText;

    protected virtual void Awake()
    {
        SetOutline();
    }

    protected void ShowText(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }
    
    private void SetOutline()
    {
        var outline = gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    public void TextTrigger()
    {
        ShowText(popUpText);
    }
    
    public abstract void Interact();
}
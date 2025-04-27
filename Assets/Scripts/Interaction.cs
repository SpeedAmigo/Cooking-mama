using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteractable
{
    [TabGroup("PopUpText")]
    [HideLabel]
    [MultiLineProperty(5)]
    [SerializeField] protected string[] popUpText;

    protected virtual void Awake()
    {
        SetOutline();
    }

    protected void ShowText(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }

    private string GetRandomText(string[] texts)
    {
        if (texts.Length <= 0) return null;
        
        string text = texts[Random.Range(0, texts.Length)];
        return text;
    }
    
    private void SetOutline()
    {
        var outline = gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    // method called by clickin mouse on object
    public void TextTrigger()
    {
        ShowText(GetRandomText(popUpText));
    }
    
    public abstract void Interact();
}
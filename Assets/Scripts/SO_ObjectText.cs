using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ObjectText", menuName = "Scriptable Objects/ObjectText")]

[InlineEditor(InlineEditorModes.GUIOnly)]
public class SoObjectText : ScriptableObject
{
    [MultiLineProperty(5)]
    public string[] popUpText;
    
    public void ShowText(string[] texts)
    {
        string text = GetRandomText(texts);
        
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }

    private string GetRandomText(string[] texts)
    {
        if (texts.Length <= 0) return null;
        
        string text = texts[Random.Range(0, texts.Length)];
        return text;
    }
}

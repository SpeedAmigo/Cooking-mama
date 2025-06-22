using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ObjectText", menuName = "Scriptable Objects/ObjectText")]

[InlineEditor(InlineEditorModes.GUIOnly)]
public class SoObjectText : ScriptableObject
{
    public bool randomText = true;
    
    [MultiLineProperty(5)] [ShowIf("randomText")]
    public string[] popUpText;
    [MultiLineProperty(5)] [HideIf("randomText")]
    public string[] chainText;
    [HideIf("randomText")]
    public float delayTime = 0.5f;
    
    public void ShowText(string[] texts)
    {
        string text = GetRandomText(texts);
        
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }

    public void ShowTextInChain(string[] texts, float delay)
    {
        if (texts == null || texts.Length == 0) return;
        
        Sequence sequence = DOTween.Sequence();

        foreach (string text in texts)
        {
            if (string.IsNullOrEmpty(text)) continue;

            sequence.AppendCallback(() =>
            {
                EventsManager.InvokeShowObjectText(text);
            });
            
            sequence.AppendInterval(delay);
        }
        
        sequence.Play();
    }

    private string GetRandomText(string[] texts)
    {
        if (texts.Length <= 0) return null;
        
        string text = texts[Random.Range(0, texts.Length)];
        return text;
    }
}

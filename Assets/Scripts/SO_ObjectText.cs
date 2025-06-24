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
    [HideIf("randomText")]
    public float durationTime = 0.5f;
    [HideIf("randomText")]
    public float initialDelayTime = 0.5f;
    
    public void ShowText(string[] texts)
    {
        string text = GetRandomText(texts);
        
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }

    public void ShowTextInChain(string[] texts, float duration, float initialDelay, float delay)
    {
        if (texts == null || texts.Length == 0) return;
        
        Sequence sequence = DOTween.Sequence();
        
        sequence.AppendInterval(initialDelay);

        foreach (string text in texts)
        {
            if (string.IsNullOrEmpty(text)) continue;

            sequence.AppendCallback(() =>
            {
                EventsManager.InvokeShowChainText(text, duration);
            });
            
            sequence.AppendInterval(duration);
            sequence.AppendInterval(delay);
        }
        
        sequence.AppendCallback(() =>
        {
            EventsManager.InvokeHideObjectText();
        });
        
        sequence.Play();
    }

    private string GetRandomText(string[] texts)
    {
        if (texts.Length <= 0) return null;
        
        string text = texts[Random.Range(0, texts.Length)];
        return text;
    }
}

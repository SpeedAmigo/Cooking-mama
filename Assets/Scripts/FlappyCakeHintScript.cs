using DG.Tweening;
using TMPro;
using UnityEngine;

public class FlappyCakeHintScript : MonoBehaviour
{
    private TMP_Text text;
    
    public void HideHint()
    {
        text = GetComponentInChildren<TMP_Text>();
        
        Color color = text.color;
        color.a = 0;
        
        text.DOColor(color, 1.5f)
            .SetDelay(4f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}

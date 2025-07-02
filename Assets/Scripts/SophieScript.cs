using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SophieScript : MinigameAbstract
{
    private Animator animator;
    [SerializeField] private Sprite purpleMask;
    [SerializeField] private Sprite greenMask;
    [SerializeField] private Sprite bubbleMask;
    [SerializeField] private SpriteRenderer maskRenderer;
    [SerializeField] private SoObjectText[] texts;
    [SerializeField] private SoObjectText[] hints;
    [SerializeField] private SoObjectText startText;
    
    public bool cleanedFace;
    public bool maskOnFace;
    public bool needToWipe;
    public int currentItemIndex;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        maskRenderer.gameObject.SetActive(false);
        
        startText.ShowChainText(startText.chainText);
    }

    public void FadeOutBubble(float magnitude)
    {
        if (!maskRenderer.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Mask renderer is disabled");
            return;
        }
        
        Color color = maskRenderer.color;
        color.a = Mathf.Clamp(color.a - magnitude, 0, 1);
        maskRenderer.color = color;

        if (color.a == 0)
        {
            needToWipe = false;
            maskRenderer.gameObject.SetActive(false);
        }
    }

    public void PutItemOnFace(int itemIndex, MaskType maskType)
    {
        if (!cleanedFace)
        {
            Debug.Log("Clean face");
            texts[0].ShowText(texts[0].popUpText);
            return;
        }
        
        if (maskOnFace)
        {
            Debug.Log("Mask on debug");
            texts[1].ShowText(texts[1].popUpText);
            return;
        }

        if (needToWipe)
        {
            Debug.Log("I need to wipe it first");
            texts[2].ShowText(texts[2].popUpText);
            return;
        }
        
        int difference = Mathf.Abs(currentItemIndex - itemIndex);

        if (difference > 1 || currentItemIndex > itemIndex)
        {
            Debug.Log("Not this one");
            texts[3].ShowText(texts[3].popUpText);
            return;
        }

        maskOnFace = true;
        currentItemIndex = itemIndex;
        ShowHint(currentItemIndex);
        maskRenderer.DOFade(1f, 0f);
        maskRenderer.gameObject.SetActive(true);
        maskRenderer.sprite = GetMask(maskType);
        
        Debug.Log("end Debug");
    }

    private void ShowHint(int index)
    {
        switch (index)
        {
            case 1:
                hints[0].ShowChainText(hints[0].chainText);
                break;
            case 2:
                hints[1].ShowChainText(hints[1].chainText);
                break;
            case 3:
                hints[2].ShowChainText(hints[2].chainText);
                break;
            case 4:
                hints[3].ShowChainText(hints[3].chainText);
                break;
            case 5:
                hints[4].ShowChainText(hints[4].chainText);
                break;
            case 7:
                hints[5].ShowChainText(hints[5].chainText);
                break;
        }
    }

    private void HideMaskAnim(int value)
    {
        maskRenderer.gameObject.SetActive(value == 1);

        if (value == 1 && maskOnFace)
        {
            maskRenderer.DOFade(1f, 0f);
            maskRenderer.sprite = bubbleMask;
            maskOnFace = false;
            needToWipe = true;
        }
    }

    private Sprite GetMask(MaskType maskType)
    {
        switch (maskType)
        {
            case MaskType.PurpleMask:
                return purpleMask;
            case MaskType.GreenMask:
                return greenMask;
            case MaskType.BubbleMask:
                return bubbleMask;
        }
        return null;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!maskOnFace) return;
        animator.SetTrigger("Bend");
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

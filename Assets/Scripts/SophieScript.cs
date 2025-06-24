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
        
        startText.ShowTextInChain(startText.chainText, startText.durationTime, startText.initialDelayTime, startText.delayTime);
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
            Debug.Log("I need to clean my face first");
            texts[0].ShowText(texts[0].popUpText);
            return;
        }
        
        if (maskOnFace)
        {
            Debug.Log("I must take this off first");
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
    }

    private void ShowHint(int index)
    {
        switch (index)
        {
            case 0:
                hints[0].ShowText(texts[0].popUpText);
                break;
            case 1:
                hints[1].ShowText(texts[1].popUpText);
                break;
            case 2:
                hints[2].ShowText(texts[2].popUpText);
                break;
            case 3:
                hints[3].ShowText(texts[3].popUpText);
                break;
            case 4:
                hints[4].ShowText(texts[4].popUpText);
                break;
            case 5:
                hints[5].ShowText(texts[5].popUpText);
                break;
            case 7:
                hints[7].ShowText(texts[7].popUpText);
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

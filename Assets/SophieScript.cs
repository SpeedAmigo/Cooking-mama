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

    public bool cleanedFace;
    public bool maskOnFace;
    public bool needToWipe;
    public int currentItemIndex;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        maskRenderer.gameObject.SetActive(false);
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
            return;
        }
        
        if (maskOnFace)
        {
            Debug.Log("I must take this off first");
            return;
        }

        if (needToWipe)
        {
            Debug.Log("I neet to wipe it first");
            return;
        }
        
        int difference = Mathf.Abs(currentItemIndex - itemIndex);

        if (difference > 1 || currentItemIndex > itemIndex)
        {
            Debug.Log("Not this one");
            return;
        }

        maskOnFace = true;
        currentItemIndex = itemIndex;
        maskRenderer.DOFade(1f, 0f);
        maskRenderer.gameObject.SetActive(true);
        maskRenderer.sprite = GetMask(maskType);
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

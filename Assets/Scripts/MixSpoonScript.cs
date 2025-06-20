using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixSpoonScript : MinigameAbstract
{
     public KitchenGameManager manager;
    
    private bool isHeld = false;
    private Renderer rend;
    private Vector2 startPos;

    [SerializeField] private BowlScript bowl;
    
    [SerializeField] private float moveMagnitude = 1f;
    [SerializeField] private Vector2 lastPos;
    [SerializeField] private Vector2 currentPos;
    [SerializeField] private Vector2 deltaPos;
    
    private void Start()
    {
        manager = KitchenGameManager.Instance;
        rend = GetComponent<Renderer>();
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        rend.sortingOrder++;
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        gameObject.transform.position = GetWorldPosition(manager.MinigameCamera);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        rend.sortingOrder--;
        gameObject.transform.DOMove(startPos, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out BowlScript script)) return;
        bowl = script;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out BowlScript script)) return;
        script.mixing = false;
        bowl = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (bowl == null) return;
        
        currentPos = transform.position;
        deltaPos = currentPos - lastPos;
        
        if (deltaPos.magnitude < moveMagnitude) return;
        
        lastPos = currentPos;
        
        if (KitchenGameManager.Instance.currentBowlItems.Count == 0) return;        

        if (bowl.currentMixCount < bowl.requiredMixCount)
        {
            bowl.mixing = true;
            bowl.currentMixCount++;
        }
        else if (bowl.currentMixCount >= bowl.requiredMixCount)
        {
            bowl.mixing = false;
        }
    }

    public override void OnPointerClick(PointerEventData eventData) { }
}

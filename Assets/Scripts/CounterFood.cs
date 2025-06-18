using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class CounterFood : MinigameAbstract
{ 
    public KitchenGameManager manager;
    
    [TabGroup("CounterFood")]
    public FoodType foodType;
    
    [TabGroup("CounterFood")]
    [SerializeField] protected bool continousUpdate = false;
    [TabGroup("CounterFood")]
    [HideIf("useBowl")]
    [SerializeField] protected bool useBoard = true;
    [TabGroup("CounterFood")]
    [HideIf("useBoard")]
    [SerializeField] protected bool useBowl = false;
    [TabGroup("CounterFood")]
    [HideIf("useBoard")]
    [SerializeField] protected bool usePot = false;
    [TabGroup("CounterFood")]
    [HideIf("useBoard")]
    [SerializeField] protected bool destroyOnUse = false;
    [TabGroup("CounterFood")]
    [ShowIf("useBoard")]
    [SerializeField] [ReadOnly] protected int requiredCuts;
    [TabGroup("CounterFood")]
    [ShowIf("useBoard")]
    [SerializeField] protected int currentCuts;
    [TabGroup("CounterFood")]
    [ShowIf("useBoard")]
    [SerializeField] [ReadOnly] protected bool onBoard;
    [TabGroup("CounterFood")]
    [ShowIf("useBoard")]
    [Required]
    [SerializeField] protected Sprite secondarySprite;
    [TabGroup("CounterFood")]
    [SerializeField] [ReadOnly] protected Vector2 lastSafePosition;
    [TabGroup("CounterFood")]
    [SerializeField] [ReadOnly] protected Vector2 startPosition;

    
    protected bool isHeld = false;
    protected SpriteRenderer renderer;
    
    public bool IsHeld => isHeld;
    public bool UseBoard => useBoard;
    public bool UseBowl => useBowl;
    public bool UsePot => usePot;
    public bool DestroyOnUse => destroyOnUse;
    public Vector2 StartPosition => startPosition;
    public bool OnBoard { get => onBoard; set => onBoard = value; }
    public int RequiredCuts => requiredCuts;
    public int CurrentCuts {get => currentCuts; set => currentCuts = value; }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        lastSafePosition = transform.position;
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        manager = KitchenGameManager.Instance;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        lastSafePosition = transform.position;
        isHeld = true;
        renderer.sortingOrder++;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        if (continousUpdate) return;
        
        gameObject.transform.position = GetWorldPosition(manager.MinigameCamera);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        renderer.sortingOrder--;

        bool isSafe = manager.CheckIfSafe(transform.position);
        
        if (!isSafe)
        {
            transform.DOMove(lastSafePosition, 0.5f);
            return;
        } 
        lastSafePosition = transform.position;
    }

    public void ChangeSprite()
    {
        renderer.sprite = secondarySprite;
    }
    
    private void LateUpdate()
    {
        if (continousUpdate && isHeld)
        {
            gameObject.transform.position = GetWorldPosition(manager.MinigameCamera);
            Debug.Log("OnLateUpdate");
        }
    }
    
    public override void OnPointerClick(PointerEventData eventData) { }
}

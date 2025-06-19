using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowlScript : MinigameAbstract
{
    public CounterFood currentFood;

    public bool mixing;
    public int currentMixCount;
    public int requiredMixCount;
    
    [SerializeField] private bool isAbove;
    [SerializeField] private GameObject halfFull;
    [SerializeField] private GameObject full;

    private bool isPlacedDown;
    private Animator animator;
    private KitchenGameManager manager;
    private SpriteRenderer renderer;
    private bool isHeld;
    private Vector2 startPos;

    private void Start()
    {
        manager = KitchenGameManager.Instance;
        startPos = transform.position;
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out CounterFood counterFood)) return;
        currentFood = counterFood;
        isAbove = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out CounterFood counterFood)) return;
        currentFood = null;
        isAbove = false;
    }

    private void Update()
    {
        animator.SetBool("Mixing", mixing);
        ChangeBowl();
        
        if (!isAbove || currentFood == null) return;

        if (!currentFood.IsHeld && currentFood.UseBowl && !isPlacedDown)
        {
            var listToAdd = KitchenGameManager.Instance.currentBowlItems;
            KitchenGameManager.Instance.AddFoodToList(currentFood.foodType, listToAdd);
            
            if (currentFood.DestroyOnUse)
            {
                Destroy(currentFood.gameObject);
            }
            else
            {
                currentFood.transform.position = currentFood.StartPosition;
            }
        }
    }

    private void ChangeBowl()
    {
        int count = KitchenGameManager.Instance.currentBowlItems.Count;

        if (count == 0)
        {
            halfFull.SetActive(false);
            full.SetActive(false);
        }
        else if (count == 1)
        {
            halfFull.SetActive(true);
            full.SetActive(false);
        }
        else if (count >= 2)
        {
            halfFull.SetActive(false);
            full.SetActive(true);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (KitchenGameManager.Instance.currentBowlItems.Count != KitchenGameManager.Instance.todayDish.bowlItems.Count)
        {
            Debug.Log("I'm missing Something");
            return;
        }
        
        isHeld = true;
        renderer.sortingOrder += 2;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isHeld) return;
        transform.position = GetWorldPosition(manager.MinigameCamera);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        renderer.sortingOrder -= 2;
        gameObject.transform.DOMove(startPos, 0.5f);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

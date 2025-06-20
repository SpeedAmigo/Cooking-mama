using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowlScript : MinigameAbstract
{
    public CounterFood currentFood;
    public PanScript panScript;

    public bool mixing;
    public int currentMixCount;
    public int requiredMixCount;
    
    [SerializeField] private bool isAbove;
    [SerializeField] private GameObject halfFull;
    [SerializeField] private GameObject full;

    private bool isPlacedDown;
    private Animator animator;
    private SpriteRenderer renderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out CounterFood counterFood)) return;
        
        this.panScript = panScript;
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
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (KitchenGameManager.Instance.currentBowlItems.Count != KitchenGameManager.Instance.todayDish.bowlItems.Count)
        {
            Debug.Log("I'm missing Something");
            return;
        }

        if (currentMixCount < requiredMixCount)
        {
            Debug.Log("I need to mix it");
            return;
        }
        
        KitchenGameManager.Instance.AddFoodToList(FoodType.MixedBowl, KitchenGameManager.Instance.currentPanItems);
        panScript.foodInside = true;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
    }

    public override void OnDrag(PointerEventData eventData)
    {
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
    }
}

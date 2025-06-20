using UnityEngine;
using UnityEngine.EventSystems;

public class PotScript : MinigameAbstract
{
    public CounterFood currentFood;
    public float timer;
    public bool firedUp;
    
    [SerializeField] private bool isAbove;
    [SerializeField] private GameObject potPasta;
    [SerializeField] private GameObject potParticles;
    
    [SerializeField] private bool pastaInside; 

    private bool isPlacedDown;

    private void Start()
    {
        potPasta.SetActive(false);
        potParticles.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out CounterFood counterFood)) return;
        if (counterFood.foodType != FoodType.Pasta) return;
        
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
        if (pastaInside && firedUp)
        {
            StartTimer();
        }

        potParticles.SetActive(firedUp);

        if (!isAbove || currentFood == null) return;
        
        if (!currentFood.IsHeld && currentFood.UseBowl && !isPlacedDown)
        {
            var listToAdd = KitchenGameManager.Instance.currentPotItems;
            KitchenGameManager.Instance.AddFoodToList(currentFood.foodType, listToAdd);
            
            pastaInside = true;
            potPasta.SetActive(true);
            
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

    private void StartTimer()
    {
        timer += Time.deltaTime;
    }

    public void FireUp()
    {
        firedUp = !firedUp;
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        int value = KitchenGameManager.Instance.IsWithinRange(
            timer, 
            KitchenGameManager.Instance.todayDish.cookingTime, 
            KitchenGameManager.Instance.todayDish.timeOffset);
        
        Debug.Log(value);

        if (value == -1)
        {
            Debug.Log("Too Early");
        }
        else if (value == 1)
        {
            Debug.Log("Damn! i over cooked it");
            pastaInside = false;
            timer = 0;
            potPasta.SetActive(false);
        }
        else if (value == 0)
        {
            KitchenGameManager.Instance.CheckForComplete(0);
        }
        
        potParticles.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }
}

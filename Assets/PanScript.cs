using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PanScript : MinigameAbstract
{
    public CounterFood currentFood;
    public float timer;
    
    [SerializeField] private bool firedUp;
    [SerializeField] private bool isAbove;
    [SerializeField] private GameObject panThing;
    [SerializeField] private GameObject panParticles;
    
    public bool foodInside; 
    private bool isPlacedDown;

    private void Start()
    {
        panThing.SetActive(false);
        panParticles.SetActive(false);
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
        if (foodInside && firedUp)
        {
            StartTimer();
        }
        
        ChangeBowl();
        
        panParticles.SetActive(firedUp);
        
        if (!isAbove || currentFood == null) return;
        
        if (!currentFood.IsHeld && currentFood.UseBowl && !isPlacedDown)
        {
            var listToAdd = KitchenGameManager.Instance.currentPanItems;
            KitchenGameManager.Instance.AddFoodToList(currentFood.foodType, listToAdd);
            
            foodInside = true;
            panThing.SetActive(true);
            
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
        int count = KitchenGameManager.Instance.currentPanItems.Count;

        if (count == 0)
        {
            panThing.SetActive(false);
        }
        else if (count >= 1)
        {
            panThing.SetActive(true);
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
            KitchenGameManager.Instance.todayDish.fryingTime, 
            KitchenGameManager.Instance.todayDish.timeOffset);

        if (value == -1)
        {
            Debug.Log("Too Early");
        }
        else if (value == 1)
        {
            Debug.Log("Damn! i over cooked it");
            foodInside = false;
            timer = 0;
            panThing.SetActive(false);
        }
        else if (value == 0)
        {
            KitchenGameManager.Instance.CheckForComplete(1);
        }
        
        panParticles.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }
}

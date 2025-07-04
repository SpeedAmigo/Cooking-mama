using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance;
    
    [SerializeField] private Camera minigameCamera;
    [SerializeField] private DayNightScript dayNightScript;
    [SerializeField] private BoxCollider2D[] safeColliders;
    [SerializeField] private Transform fridgeParent;
    [SerializeField] private SoObjectText minigameStartText;

    public List<FoodType> currentBowlItems = new();
    public List<FoodType> currentPanItems  = new();
    public List<FoodType> currentPotItems  = new();

    public bool potReady;
    public bool panReady;
    
    public Camera MinigameCamera { get => minigameCamera;}

    [TabGroup("TodayDish")]
    public Dish todayDish;

    [TabGroup("dishRecipes")]
    public List<Dish> dishRecipes = new();
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start()
    {
        todayDish = GetTodayDish(dayNightScript.GetDayCount());
        minigameStartText.chainText = new[]
        {
            $"Should we make something simple, perhaps a {todayDish.dishName.ToString()}?"
        };
        
        minigameStartText.ShowChainText(minigameStartText.chainText);
    }
    
    private Dish GetTodayDish(int currentDay)
    {
        int day = currentDay - 1;
        
        for (int i = 0; i < dishRecipes.Count; i++)
        {
           return dishRecipes[day];
        }
        
        return null;
    }
    
    public void AddFoodToList(FoodType foodType, List<FoodType> vessel)
    {
        if (vessel.Contains(foodType)) return;
        vessel.Add(foodType);
        CheckIfCorrect();
    }

    public void CheckForComplete(int type)
    {
        if (type == 0)
        {
            potReady = true;
            DisplayDish();
        }
        
        if (type == 1)
        {
            panReady = true;
            DisplayDish();
        }
    }

    private void DisplayDish()
    {
        if (panReady && potReady)
        {
            todayDish.dishToDisplay.SetActive(true);
        }
    }

    public int IsWithinRange(float time, float requiredTime, float offset)
    {
        if (time < requiredTime - offset)
        {
            return -1;
        }
        else if (time > requiredTime + offset)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void ClearFoodLists()
    {
        currentBowlItems.Clear();
        currentPanItems.Clear();
        currentPotItems.Clear();
    }

    private void CheckIfCorrect()
    {
        bool bowlCorrect = CompareLists(currentBowlItems, todayDish.bowlItems);
        bool panCorrect  = CompareLists(currentPanItems, todayDish.panItems);
        bool potCorrect  = CompareLists(currentPotItems, todayDish.potItems);

        if (!bowlCorrect || !panCorrect || !potCorrect)
        {
            Debug.Log("I must have done something wrong");
            ClearFoodLists();
        }
    }

    private bool CompareLists(List<FoodType> currentList, List<FoodType> correctList)
    {
        if (currentList.Count > correctList.Count) return false;

        for (int i = 0; i < currentList.Count; i++)
        {
            if (!correctList.Contains(currentList[i])) return false;
        }
        
        return true;
    }

    public bool CheckIfSafe(Transform objTransform)
    {
        Vector2 pos2D = new Vector2(objTransform.position.x, objTransform.position.y);
        
        foreach (var collider in safeColliders)
        {
            Bounds bounds = collider.bounds;
            
            Vector2 min = new Vector2(bounds.min.x, bounds.min.y);
            Vector2 max = new Vector2(bounds.max.x, bounds.max.y);

            if (pos2D.x >= min.x && pos2D.x <= max.x 
                && pos2D.y >= min.y && pos2D.y <= max.y)
            {
                TryReparentIfSafe(objTransform);
                return true;
            }
        }
        return false;
    }
    
    private void TryReparentIfSafe(Transform objPosition)
    {
        Vector2 pos = objPosition.position;

        for (int i = 0; i < safeColliders.Length; i++)
        {
            Bounds bounds = safeColliders[i].bounds;

            if (bounds.Contains(pos))
            {
                if (i == 0 || i == 1)
                {
                    objPosition.SetParent(null);
                }
                else
                {
                    objPosition.SetParent(fridgeParent);
                }
                return;
            }
        }
    }
}

[Serializable]
public class Dish
{
    public float timeOffset;
    public float cookingTime;
    public float fryingTime;
    
    public GameObject dishToDisplay;
    public DishType dishName;
    public List<FoodType> bowlItems;
    public List<FoodType> panItems;
    public List<FoodType> potItems;
}

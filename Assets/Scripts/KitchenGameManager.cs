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

    public List<FoodType> currentBowlItems = new();
    public List<FoodType> currentPanItems  = new();
    public List<FoodType> currentPotItems  = new();
    
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
        Debug.Log(vessel.Count.ToString());
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

    public bool CheckIfSafe(Vector3 position)
    {
        Vector2 pos2D = new Vector2(position.x, position.y);
        
        foreach (var collider in safeColliders)
        {
            Bounds bounds = collider.bounds;
            
            Vector2 min = new Vector2(bounds.min.x, bounds.min.y);
            Vector2 max = new Vector2(bounds.max.x, bounds.max.y);

            if (pos2D.x >= min.x && pos2D.x <= max.x 
                && pos2D.y >= min.y && pos2D.y <= max.y)
            {
                return true;
            }
        }
        return false;
    }
}

[Serializable]
public class Dish
{
    public float cookingTime;
    public float fryingTime;
    
    public DishType dishName;
    public List<FoodType> bowlItems;
    public List<FoodType> panItems;
    public List<FoodType> potItems;
}

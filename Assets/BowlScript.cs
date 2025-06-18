using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    public CounterFood currentFood;
    public HashSet<FoodType> foodInBowl = new();
    
    [SerializeField] private bool isAbove;

    private bool isPlacedDown;
    
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
}

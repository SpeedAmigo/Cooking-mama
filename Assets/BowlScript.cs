using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    public CounterFood currentFood;
    
    [SerializeField] private bool isAbove;
    [SerializeField] private GameObject halfFull;
    [SerializeField] private GameObject full;

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
            
            ChangeBowl();
            
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
}

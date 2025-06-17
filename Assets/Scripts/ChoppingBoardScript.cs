using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> cuttedFood;
    [SerializeField] private bool isAbove;
    public CounterFood currentFood;

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
        
        if (!currentFood.IsHeld && currentFood.UseBoard && !isPlacedDown)
        {
            currentFood.transform.position = transform.position;
            currentFood.OnBoard = true;
            isPlacedDown = true;
        }
        else if (currentFood.IsHeld)
        {
            isPlacedDown = false;
            currentFood.OnBoard = false;
        }
    }

    public void SpawnCuttedFood()
    {
        if (currentFood == null) return;
        
        switch (currentFood.foodType)
        {
            case FoodType.Apple:
                Instantiate(cuttedFood[0], transform.position, Quaternion.identity);
                break;
            case FoodType.Bread:
                Instantiate(cuttedFood[1], transform.position, Quaternion.identity);
                break;
            case FoodType.Meat:
                Instantiate(cuttedFood[2], transform.position, Quaternion.identity);
                break;
        }
        
        currentFood.transform.position = currentFood.StartPosition;
        isAbove = false;
        isPlacedDown = false;
        currentFood.OnBoard = false;
        currentFood.CurrentCuts = 0;
        currentFood = null;
    }
}

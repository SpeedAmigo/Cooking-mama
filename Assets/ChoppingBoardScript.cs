using System;
using UnityEngine;

public class ChoppingBoardScript : MonoBehaviour
{
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
}

using System;
using System.Collections.Generic;
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
            StartTimer(true);
        }
        
        if (!isAbove || currentFood == null) return;
        
        if (!currentFood.IsHeld && currentFood.UseBowl && !isPlacedDown)
        {
            var listToAdd = KitchenGameManager.Instance.currentPotItems;
            KitchenGameManager.Instance.AddFoodToList(currentFood.foodType, listToAdd);
            
            pastaInside = true;
            potPasta.SetActive(true);
            potParticles.SetActive(true);
            
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

    private void StartTimer(bool value)
    {
        if (!value) return;
        timer += Time.deltaTime;
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        StartTimer(false);
        timer = 0;
        pastaInside = false;
        potPasta.SetActive(false);
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

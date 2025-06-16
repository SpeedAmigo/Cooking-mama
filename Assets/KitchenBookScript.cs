using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class KitchenBookScript : MinigameAbstract, IInputHandler
{
    [Required]
    [SerializeField] private GameObject kitchenBookCanvas;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        InputManager.Instance.RegisterHandler(this);
        kitchenBookCanvas.SetActive(true);
    }
    
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.Instance.UnregisterHandler(this);
            kitchenBookCanvas.SetActive(false);
        }
    }
    
    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnPointerUp(PointerEventData eventData) {}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUILogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> windows;
    public List<GameObject> slots = new();
    public RectTransform rectTransform;

    private WindowHandler _windowHandler = new();

    private void OnEnable()
    {
        EventsManager.AddItemToUI += AddItemToSlot;
    }
    private void OnDisable()
    {
        EventsManager.AddItemToUI -= AddItemToSlot;
        EventsManager.AddInventoryReference -= RestoreItems;
    }
    
    public void AddItemToSlot(ItemInstance itemInstance)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                GameObject item = new GameObject("Item");
                item.transform.SetParent(slots[i].transform);
                
                item.AddComponent<DragDrop>().itemInstance = itemInstance;
                var image = item.AddComponent<Image>();
                image.sprite = itemInstance.sprite;
                image.transform.localScale = new Vector3(1, 1, 1);
                image.preserveAspect = true;
                break;
            }
        }
    }

    private void RestoreItems(SO_Inventory inventory)
    {
        foreach (ItemInstance item in inventory.items)
        {
            AddItemToSlot(item);
        }
    }

    private void Start()
    {
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
        
        EventsManager.AddInventoryReference += RestoreItems;
    }
    
    private void Update()
    {
        var inputTab = Input.GetKeyDown(KeyCode.Tab);
        var inputI = Input.GetKeyDown(KeyCode.I);

        if (inputTab || inputI)
        {
            _windowHandler.WindowToggle(windows, GameState.InGame, GameState.Inventory);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SO_Inventory SoInventory;
    private InventoryUILogic _inventoryUILogic;

    private void OnEnable()
    {
        EventsManager.RemoveItemEvent += RemoveItem;
        EventsManager.AddItemEvent += AddItem;
    }
    
    private void OnDisable()
    {
        EventsManager.RemoveItemEvent -= RemoveItem;
        EventsManager.AddItemEvent -= AddItem;
    }

    private void Start()
    {
        _inventoryUILogic = GetComponentInChildren<InventoryUILogic>();
        _inventoryUILogic.CreateItemSlots(SoInventory.maxInventorySize);
        _inventoryUILogic.RestoreItems(SoInventory);
    }
    
    private void AddItem(ItemScript item)
    {
        if (SoInventory.items.Count < SoInventory.maxInventorySize)
        {
            SoInventory.items.Add(item.itemInstance);
            _inventoryUILogic.AddItemToSlot(item.itemInstance);
            Destroy(item.gameObject);
        }
    }

    private void RemoveItem(ItemInstance itemInstance)
    {
        DropItem(itemInstance, transform.root.position);
        SoInventory.items.Remove(itemInstance);
    }
    
    private void DropItem(ItemInstance itemInstance, Vector3 spawnPosition)
    {
        Vector3 spawnOffset = new Vector2(0f, -1f);
        
        GameObject droppedItem = new GameObject("DroppedItem");
        
        ItemScript itemScript = droppedItem.AddComponent<ItemScript>();
        itemScript.itemInstance = itemInstance;
        
        BoxCollider2D boxCollider2D = droppedItem.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        
        SpriteRenderer spriteRenderer = droppedItem.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemInstance.sprite;

        droppedItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        droppedItem.transform.position = spawnPosition + spawnOffset;
    }
}   

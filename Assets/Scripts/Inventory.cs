using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new();

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

    public void AddItem(ItemInstance itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(ItemInstance itemToRemove)
    {
        DropItem(itemToRemove, transform.root.position);
        items.Remove(itemToRemove);
    }

    private GameObject DropItem(ItemInstance itemInstance, Vector3 spawnPosition)
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
        
        return droppedItem;
    }
}   

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new();

    private void OnEnable()
    {
        ItemScript.AddItemEvent += AddItem;
    }

    private void OnDisable()
    {
        ItemScript.AddItemEvent -= AddItem;
    }

    public void AddItem(ItemInstance itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }
    
}

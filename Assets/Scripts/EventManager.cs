using System;
using UnityEngine;

public static class EventsManager
{
    public static event Action<ItemScript> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;
    public static event Action<ItemInstance> AddItemToUI;
    public static event Action<SO_Inventory> AddInventoryReference;

    public static void InvokeAddItemEvent(ItemScript itemScript)
    {
        AddItemEvent?.Invoke(itemScript);
    }

    public static void InvokeRemoveItemEvent(ItemInstance itemInstance)
    {
        RemoveItemEvent?.Invoke(itemInstance);
    }

    public static void InvokeAddItemToUI(ItemInstance itemInstance)
    {
        AddItemToUI?.Invoke(itemInstance);
    }
    
    public static void InvokeAddInventoryReference(SO_Inventory inventory)
    {
        AddInventoryReference?.Invoke(inventory);
    }
}

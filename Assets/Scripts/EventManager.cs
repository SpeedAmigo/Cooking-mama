using System;

public static class EventsManager
{
    public static event Action<ItemInstance> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;
    
    public static void InvokeAddItemEvent(ItemInstance itemInstance)
    {
        AddItemEvent?.Invoke(itemInstance);
    }

    public static void InvokeRemoveItemEvent(ItemInstance itemInstance)
    {
        RemoveItemEvent?.Invoke(itemInstance);
    }
}

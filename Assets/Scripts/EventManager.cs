using System;
using UnityEngine;

public static class EventsManager
{
    public static event Action<ItemScript> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;

    public static event Action<Vector2> TileClickedEvent;
    public static void InvokeAddItemEvent(ItemScript itemScript)
    {
        AddItemEvent?.Invoke(itemScript);
    }

    public static void InvokeRemoveItemEvent(ItemInstance itemInstance)
    {
        RemoveItemEvent?.Invoke(itemInstance);
    }

    public static void InvokeTileClickedEvent(Vector2 position)
    {
        TileClickedEvent?.Invoke(position);
    }
}

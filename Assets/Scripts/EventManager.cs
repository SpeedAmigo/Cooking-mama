using System;
using UnityEngine;

public static class EventsManager
{
    public static event Action<ItemScript> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;
    public static event Action<bool> LightToggleEvent;
    public static event Action OnDrawerOpenEvent;

    public static void InvokeAddItemEvent(ItemScript itemScript)
    {
        AddItemEvent?.Invoke(itemScript);
    }

    public static void InvokeRemoveItemEvent(ItemInstance itemInstance)
    {
        RemoveItemEvent?.Invoke(itemInstance);
    }

    public static void InvokeLightToggleEvent(bool state)
    {
        LightToggleEvent?.Invoke(state);
    }

    public static void InvokeOnDrawerOpenEvent()
    {
        OnDrawerOpenEvent?.Invoke();
    }
}

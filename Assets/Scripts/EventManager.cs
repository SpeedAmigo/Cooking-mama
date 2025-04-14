using System;
using UnityEngine;

public static class EventsManager
{
    public static event Action<ItemScript> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;
    public static event Action<bool> LightToggleEvent;
    public static event Action<BedroomMinigameManager> OnGetBedroomMinigameManager;
    public static event Action BedroomMinigameCompleteEvent;
    public static event Action<string> ShowObjectText;

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

    public static void InvokeOnGetBedroomManager(BedroomMinigameManager bedroomMinigameManager)
    {
        OnGetBedroomMinigameManager?.Invoke(bedroomMinigameManager);
    }

    public static void InvokeBedroomMinigameCompleteEvent()
    {
        BedroomMinigameCompleteEvent?.Invoke();
    }

    public static void InvokeShowObjectText(string text)
    {
        ShowObjectText?.Invoke(text);
    }
}

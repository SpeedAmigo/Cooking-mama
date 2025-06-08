using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class EventsManager
{
    public static event Action<ItemScript> AddItemEvent;
    public static event Action<ItemInstance> RemoveItemEvent;
    public static event Action<bool> LightToggleEvent;
    public static event Action<BedroomMinigameManager> OnGetBedroomMinigameManager;
    public static event Action BedroomMinigameCompleteEvent;
    public static event Action<string> ShowObjectText;
    public static event Action HideObjectText;
    public static event Action<Color, float> LightColorChangeEvent;
    public static event Action ChangeTimeEvent;
    public static event Action<Light2D> LightReference;

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

    public static void InvokeLightColorChange(Color color, float duration)
    {
        LightColorChangeEvent?.Invoke(color, duration);
    }

    public static void InvokeChangeTimeEvent()
    {
        ChangeTimeEvent?.Invoke();
    }

    public static void InvokeLightReference(Light2D light)
    {
        LightReference?.Invoke(light);
    }

    public static void InvokeHideObjectText()
    {
        HideObjectText?.Invoke();
    }
}

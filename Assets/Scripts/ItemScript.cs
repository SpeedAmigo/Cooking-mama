using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickable
{
    [SerializeField] private ItemData itemData;
    public ItemInstance itemInstance;

    public void Start()
    {
        //base.Start();

        if (itemData)
        {
            itemInstance = new ItemInstance(itemData);
        }
    }
    
    public void Pick()
    {
        if (GameStateManager.CurrentGameState == GameState.InGame)
        {
            EventsManager.InvokeAddItemEvent(this);
        }
    }
}

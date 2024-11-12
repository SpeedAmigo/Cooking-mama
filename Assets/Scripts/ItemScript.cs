using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickable
{
    [SerializeField] private ItemData itemData;
    public ItemInstance itemInstance;
    
    public static event Action<ItemInstance> AddItemEvent;
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
        AddItemEvent?.Invoke(itemInstance);
        Destroy(gameObject);
    }
}

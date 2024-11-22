using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SO Inventory", menuName = "SO Inventory")]
public class SO_Inventory : ScriptableObject
{
    public List<ItemInstance> items = new();

    [SerializeField] private  InventorySize _size = InventorySize.Small;

    [ReadOnly]
    public int maxInventorySize;

    private void OnValidate()
    {
        maxInventorySize = (int)_size;
    }
}

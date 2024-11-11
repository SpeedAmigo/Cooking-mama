using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class SoInventory : ScriptableObject
{
    public List<ItemData> items = new();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ItemData", menuName = "Items", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    
    public bool isStackAble;

    public int startingValue1;
    public int startingValue2;
}

using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public Sprite sprite;

    public int value1;
    public int value2;

    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        sprite = itemData.icon;
        value1 = itemData.startingValue1;
        value2 = itemData.startingValue2;
    }
}

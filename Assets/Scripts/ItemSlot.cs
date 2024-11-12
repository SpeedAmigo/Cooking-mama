using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        DragDrop dragDrop = droppedItem.GetComponent<DragDrop>();
        dragDrop.parentAfterDrag = transform;
    }
}

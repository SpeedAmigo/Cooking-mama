using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUILogic : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    public List<GameObject> slots = new();

    private WindowHandler _windowHandler = new();

    private void OnEnable()
    {
        ItemScript.AddItemEvent += AddItemToSlot;
    }

    private void OnDisable()
    {
        ItemScript.AddItemEvent -= AddItemToSlot;
    }
    
    public void AddItemToSlot(ItemInstance itemInstance)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                GameObject item = new GameObject("Item");
                item.transform.SetParent(slots[i].transform);

                item.AddComponent<DragDrop>();
                var image = item.AddComponent<Image>();
                image.sprite = itemInstance.sprite;
                image.transform.localScale = new Vector3(1, 1, 1);
                image.preserveAspect = true;
                break;
            }
        }
    }

    private void Start()
    {
        grid.SetActive(false);
    }
    
    private void Update()
    {
        var inputTab = Input.GetKeyDown(KeyCode.Tab);
        var inputI = Input.GetKeyDown(KeyCode.I);

        if (inputTab || inputI)
        {
            _windowHandler.WindowToggle(grid, GameState.InGame, GameState.Inventory);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUILogic : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotprefab;
    [SerializeField] private List<GameObject> windows;
    
    public List<GameObject> slots = new();
    
    private WindowHandler _windowHandler = new(); 
    private  GridLayoutGroup _layoutGroup;
    [HideInInspector] public RectTransform _rectTransform;

    public void AddItemToSlot(ItemInstance itemInstance)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                GameObject item = new GameObject("Item");
                item.transform.SetParent(slots[i].transform);
                
                item.AddComponent<DragDrop>().itemInstance = itemInstance;
                var image = item.AddComponent<Image>();
                image.sprite = itemInstance.sprite;
                image.transform.localScale = new Vector3(1, 1, 1);
                image.preserveAspect = true;
                break;
            }
        }
    }

    public void CreateItemSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(itemSlotprefab, _layoutGroup.transform);
            slots.Add(slot);
        }
    }

    public void RestoreItems(SO_Inventory inventory)
    {
        foreach (ItemInstance item in inventory.items)
        {
            AddItemToSlot(item);
        }
    }

    private void Awake()
    {
        _layoutGroup = GetComponentInChildren<GridLayoutGroup>();
        _rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            windows.Add(transform.GetChild(i).gameObject);
        }
        
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
    }
    
    private void Update()
    {
        var inputTab = Input.GetKeyDown(KeyCode.Tab);
        var inputI = Input.GetKeyDown(KeyCode.I);

        if (inputTab || inputI)
        {
            _windowHandler.WindowToggle(windows, GameState.InGame, GameState.Inventory);
        }
    }
}

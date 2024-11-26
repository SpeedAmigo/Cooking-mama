using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    
    [SerializeField] private RectTransform _rectTransform;
    private Transform _itemParent;
    [SerializeField] private Image _image;
    public ItemInstance itemInstance;

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        var inventoryUILogic = GetComponentInParent<InventoryUILogic>();
        _itemParent = inventoryUILogic.transform;
        _rectTransform = inventoryUILogic._rectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(_itemParent);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsInsideBackground())
        {
            transform.SetParent(parentAfterDrag);
        }
        else
        {
            Destroy(gameObject);
            EventsManager.InvokeRemoveItemEvent(itemInstance);
        }
        _image.raycastTarget = true;
    }

    private bool IsInsideBackground()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, null, out localPoint);
        return _rectTransform.rect.Contains(localPoint);
    }
}

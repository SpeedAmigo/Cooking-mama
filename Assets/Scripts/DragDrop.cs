using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    
    [SerializeField] private RectTransform _rectTransform;
    private Transform _itemParent;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _itemParent = GetComponentInParent<InventoryUILogic>().transform;
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
            Debug.Log($"Dropped {gameObject.name}");
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

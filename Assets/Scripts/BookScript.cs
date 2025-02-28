using UnityEngine;
using UnityEngine.EventSystems;

public class BookScript : MonoBehaviour, IPointerClickHandler
{
    private ShelfManager _shelfManager;
    public Transform SnapTarget { get; private set; }

    [SerializeField] private Transform _oldParent;
    [SerializeField] private Transform _currentParent;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _shelfManager.GrabBook(gameObject);
    }

    public void SetNewParent(Transform newParent)
    {
        _oldParent = _currentParent;
        _currentParent = newParent;
        
        transform.SetParent(newParent);
    }

    public Transform GetPreviousParent()
    {
        if (_oldParent == null) return null;
        
        return _oldParent;
    }
    
    // essential for keeping _shelfManager private
    public void ManagerReference(ShelfManager manager)
    {
        _shelfManager = manager;
    }
    
    // needed for picking target
    public void SetSnapTarget(Transform target)
    {
        SnapTarget = target;
    }
}

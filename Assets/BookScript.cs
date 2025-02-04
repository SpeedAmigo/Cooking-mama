using UnityEngine;
using UnityEngine.EventSystems;

public class BookScript : MonoBehaviour, IPointerClickHandler
{
    private ShelfManager _shelfManager;
    private bool _dragging;
    private Transform _snapTarget;
    
    private Vector3 offset = new Vector3(0, 1f, 0);
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_dragging && _shelfManager.heldBook.Count >= 1)
        {
            _shelfManager.SwapBooks(_shelfManager.heldBook[0], gameObject);
            return;
        }

        if (!_dragging && _shelfManager.heldBook.Count < 1)
        {
            _dragging = true;
            _shelfManager.heldBook.Add(gameObject);
            transform.SetParent(_shelfManager.booksParent.transform);
            return;
        }
        
        _dragging = false;
        _shelfManager.heldBook.Remove(gameObject);
        transform.SetParent(_shelfManager.booksSlots.transform);
        TrySnapToTarget();
    }

    // this try to snap book to target if released close enough
    public void TrySnapToTarget()
    {
        if (!_dragging && GetDistance() <= _shelfManager.snapDistance)
        {
            gameObject.transform.position = _snapTarget.position;
            Debug.Log("Snapped to target");
        }
    }

    // essential for measuring distance between book and its snapping target
    private float GetDistance()
    {
        return Vector3.Distance(gameObject.transform.position, _snapTarget.position);
    }

    public void SetDragging(bool dragging)
    {
        _dragging = dragging;
    }

    // essential for keeping _shelfManager private
    public void ManagerReference(ShelfManager manager)
    {
        _shelfManager = manager;
    }
    
    // needed for picking target
    public void SetSnapTarget(Transform target)
    {
        _snapTarget = target;
    }

    void Update()
    {
        if (_dragging)
        {
            transform.position = Input.mousePosition + offset;
        }
    }
}

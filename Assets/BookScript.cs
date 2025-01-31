using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookScript : MonoBehaviour, IPointerClickHandler
{
    [Range(0f, 300f)]
    public float snapDistance;

    private bool _dragging;
    
    [SerializeField] private Transform snapTarget;
    [SerializeField] private List<Transform> startPoints;
    
    
    public void OnPointerClick(PointerEventData eventData) 
    {
        if (!_dragging)
        {
            _dragging = true;
            return;
        }
        
        _dragging = false;
        TrySnapToTarget();
    }

    // this try to snap book to target if released close enough
    private void TrySnapToTarget()
    {
        if (!_dragging && GetDistance() <= snapDistance)
        {
            gameObject.transform.position = snapTarget.position;
            Debug.Log("Snapped to target");
        }
    }

    // essential for measuring distance between book and its snapping target
    private float GetDistance()
    {
        return Vector3.Distance(gameObject.transform.position, snapTarget.position);
    }
    
    // needed for picking target
    private Transform PickSnapTarget()
    {
        Transform pickedTarget = startPoints[Random.Range(0, startPoints.Count)];
        startPoints.Remove(pickedTarget);
        return pickedTarget;
    }

    // set start position for the book
    private Transform PickStartPoint()
    {
        Transform pickedStartPoint = startPoints[Random.Range(0, startPoints.Count)];
        return pickedStartPoint;
    }

    private void Start()
    {
        snapTarget = PickSnapTarget();
        gameObject.transform.position = PickStartPoint().position;
    }
    void Update()
    {
        if (_dragging)
        {
            transform.position = Input.mousePosition;
        }
    }
}

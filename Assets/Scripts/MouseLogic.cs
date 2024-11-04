using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    private PlayerScript _playerScript;
    private Vector3 _screenPosition;

    [SerializeField] private float _maxDistance;
    
    private void ObjectInteraction(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 15);

        if (!hit.collider) return;
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractAble interactable))
            {
                interactable.Interact();
            }
        }
    }

    private bool IsWithinRange(Vector2 mousePosition, Vector2 characterPosition)
    {
        float range = Vector2.Distance(mousePosition, characterPosition);
        
        return range <= _maxDistance;
    }

    private void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
    
    private void Update()
    {
        _screenPosition = Input.mousePosition;
        Vector2 rayPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        
        if (Input.GetMouseButtonDown(0) && IsWithinRange(rayPosition, _playerScript.transform.position))
        {
            ObjectInteraction(rayPosition);
        }
    }
    
    // creates debug line to maxDistance
    private void OnDrawGizmos()
    {
        if (_playerScript == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerScript.transform.position, _maxDistance);
    }
}

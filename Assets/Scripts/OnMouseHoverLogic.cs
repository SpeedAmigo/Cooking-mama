using UnityEngine;

public class OnMouseHoverLogic
{
    private OutlineFx.OutlineFx _lastOutlinedObject;
    
    private IInteractable _lastInteractable;
    private IPickable _lastPickable;
    
    
    public void OnMouseHover(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 15);

        // Check if the raycast hit an object with an outline
        if (hit.collider && hit.collider.isTrigger && hit.collider.gameObject.TryGetComponent(out OutlineFx.OutlineFx outlineFx))
        {
            // If there's a new object, disable the outline on the last object
            if (_lastOutlinedObject && _lastOutlinedObject != outlineFx)
            {
                _lastOutlinedObject.enabled = false;
            }

            // Enable the outline on the current object and update the last outlined object
            outlineFx.enabled = true;
            _lastOutlinedObject = outlineFx;
        }
        else
        {
            // If no object is hit, disable the outline on the last outlined object
            if (_lastOutlinedObject)
            {
                _lastOutlinedObject.enabled = false;
                _lastOutlinedObject = null;
            }
        }
    }
    public void MouseHover(Vector2 position, Texture2D normalCursor, Texture2D interactCursor, Texture2D pickCursor)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 15);
        
        // Check if the raycast hit an object with an triggerCollider
        if (hit.collider && hit.collider.isTrigger)
        {
            var iInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            var iPickable = hit.collider.gameObject.GetComponent<IPickable>();

            if (iInteractable != null)
            {
                Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
            }

            if (iPickable != null)
            {
                Cursor.SetCursor(pickCursor, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnMouseHover(Vector2 position, Texture2D normalCursor, Texture2D interactCursor, Texture2D pickCursor)
    {
        MouseHover(position, normalCursor, interactCursor, pickCursor);
    }
}

using UnityEngine;

public class OnMouseHoverLogic
{
    private OutlineFx.OutlineFx _lastOutlinedObject;
    
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
}
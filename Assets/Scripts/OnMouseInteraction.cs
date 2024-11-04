using UnityEngine;

public class OnMouseInteraction
{
    public void ObjectInteraction(Vector2 position)
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
}

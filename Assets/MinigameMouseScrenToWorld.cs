using UnityEngine;

public class MinigameMouseScrenToWorld : MonoBehaviour
{
    public Camera minigameCamera;
    public Transform background;
    
    private IMinigameInteractable lastHitObject;

    private MinigameMouseHelper mouseHelper = new();
    
    private void HandleRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseHelper.localPos, Vector2.zero);

        if (hit.collider != null)
        {
            IMinigameInteractable interactable = hit.collider.gameObject.GetComponent<IMinigameInteractable>();

            if (interactable != null)
            {
                if (interactable != lastHitObject)
                {
                    lastHitObject?.OnRaycastExit();
                    interactable.OnRaycastEnter();
                }
                
                interactable.OnRaycastOver();
                lastHitObject = interactable;
            }
            else if (lastHitObject != null)
            {
                lastHitObject.OnRaycastExit();
                lastHitObject = null;
            }
        }
    }

    private void Update()
    {
        if (minigameCamera == null) return;
        
        mouseHelper.HandleMinigameMouse(minigameCamera, background);
        HandleRaycast();
    }
}

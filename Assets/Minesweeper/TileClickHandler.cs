using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    [SerializeField] private Camera minigameCamera;
    [SerializeField] private Transform field;
    
    private int lastMouseButton = -1;
    private MinigameMouseHelper mouseHelper = new();

    private void HandleClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseHelper.localPos, Vector2.zero);

        if (hit.collider != null)
        {
            TileScript tile = hit.collider.GetComponent<TileScript>();
            if (tile != null)
            {
                if (lastMouseButton == 0)
                {
                    tile.MouseLeftClick();
                }
                else
                {
                    tile.MouseRightClick();
                }
            }
        }
    }
    
    void Update()
    {
        mouseHelper.HandleMinigameMouse(minigameCamera, field);
        
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseButton = 0;
            HandleClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            lastMouseButton = 1;
            HandleClick();
        }
    }
}




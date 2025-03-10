using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClickHandler : MonoBehaviour
{
    [SerializeField] private Camera minigameCamera;
    [SerializeField] private Transform field;
    
    private int lastMouseButton = -1;

    private void HandleClick()
    {
        // this gets local positions from field to ensure that mouse is getting right tile
        Vector2 localPos = field.InverseTransformPoint((Vector2)minigameCamera.ScreenToWorldPoint(Input.mousePosition));
        RaycastHit2D hit = Physics2D.Raycast(localPos, Vector2.zero);

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




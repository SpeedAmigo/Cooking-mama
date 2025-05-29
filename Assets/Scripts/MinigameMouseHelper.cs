using UnityEngine;

public class MinigameMouseHelper
{
    public Vector2 scaleFactor = new (1f, 1f);
    public Vector2 localPos;
    
    public Vector2 HandleMinigameMouse(Camera minigameCam, Transform bg)
    {
        Vector2 worldPos = minigameCam.ScreenToWorldPoint(Input.mousePosition);
        localPos = bg.InverseTransformPoint(worldPos) * scaleFactor;
        
        return localPos;
    }
}

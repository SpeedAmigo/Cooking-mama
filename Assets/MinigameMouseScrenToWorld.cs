using UnityEngine;

public class MinigameMouseScrenToWorld : MonoBehaviour
{
    public Camera minigameCamera;
    public Transform background;

    private Vector2 scaleFactor = new Vector2(1.2f, 1.2f);

    private void HandleMinigameMouse(Camera minigameCam, Transform bg)
    {
        Vector2 worldPos = minigameCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 localPos = bg.InverseTransformPoint(worldPos) * scaleFactor;
        
        RaycastHit2D hit = Physics2D.Raycast(localPos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private void Update()
    {
        HandleMinigameMouse(minigameCamera, background);
    }
}

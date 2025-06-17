using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance;
    
    [SerializeField] private Camera minigameCamera;
    public Camera MinigameCamera { get => minigameCamera;}
    
    [SerializeField] private BoxCollider2D[] safeColliders;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public bool CheckIfSafe(Vector3 position)
    {
        Vector2 pos2D = new Vector2(position.x, position.y);
        
        foreach (var collider in safeColliders)
        {
            Bounds bounds = collider.bounds;
            
            Vector2 min = new Vector2(bounds.min.x, bounds.min.y);
            Vector2 max = new Vector2(bounds.max.x, bounds.max.y);

            if (pos2D.x >= min.x && pos2D.x <= max.x 
                && pos2D.y >= min.y && pos2D.y <= max.y)
            {
                return true;
            }
        }
        return false;
    }
}

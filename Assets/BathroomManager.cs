using System.Collections.Generic;
using UnityEngine;

public class BathroomManager : MonoBehaviour
{
    public static BathroomManager Instance;
    
    public Camera mainCamera;
    public List<BathroomItem> bathroomItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
}

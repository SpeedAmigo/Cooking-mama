using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReferenceScript : MonoBehaviour
{
    private Tilemap tilemap;
    
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        WorldMapManager.Instance.Tilemap = tilemap;
    }
}

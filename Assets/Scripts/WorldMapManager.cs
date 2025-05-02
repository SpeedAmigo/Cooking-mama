using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class WorldMapManager : MonoBehaviour
{
    public static WorldMapManager Instance { get; set; } 

    [SerializeField] private Tilemap _tilemap;
    public Tilemap Tilemap {set { _tilemap = value; } }
    
    [SerializeField] private List<TileData> _tileDataList;

    private Dictionary<TileBase, TileData> _tileDataDictionary;
    
    [SerializeField] [ReadOnly] private FloorType currentFloorType;
    
    private void Awake()
    {
        //singleton setup
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        //DontDestroyOnLoad(gameObject);
        
        // Initialize tile data dictionary
        _tileDataDictionary = new Dictionary<TileBase, TileData>();
        foreach (var tileData in _tileDataList)
        {
            foreach (var tile in tileData.ruleTiles)
            {
                _tileDataDictionary.Add(tile, tileData); // assign each tile to desired tileData (grass, rock, etc.)
            }
        }
    }
    
    public FloorType GetFloorType(Vector3 worldPosition)
    {
        Vector3Int cellPosition = _tilemap.WorldToCell(worldPosition);
        TileBase tile = _tilemap.GetTile(cellPosition);
        
        FloorType floorType = _tileDataDictionary[tile].floorType;
        
        currentFloorType = floorType;
        
        return floorType;
    }
}

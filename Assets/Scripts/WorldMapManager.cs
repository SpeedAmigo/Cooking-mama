using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldMapManager : MonoBehaviour
{
    public static WorldMapManager Instance { get; set; }
    
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<TileData> _tileDataList;

    private Dictionary<TileBase, TileData> _tileDataDictionary;

    private void Awake()
    {
        //singleton setup
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Initialize tile data dictionary
        _tileDataDictionary = new Dictionary<TileBase, TileData>();
        foreach (var tileData in _tileDataList)
        {
            foreach (var tile in tileData.tiles)
            {
                _tileDataDictionary.Add(tile, tileData); // assign each tile to desired tileData (grass, rock, etc.)
            }
        }
    }

    public AudioClip GetCurretnAudioClip(Vector3 worldPosition)
    {
        Vector3Int cellPosition = _tilemap.WorldToCell(worldPosition); // store player position and put it into tile map
        TileBase tile = _tilemap.GetTile(cellPosition); // gets current tile player is standing
        
        int index = Random.Range(0, _tileDataDictionary[tile].audioClips.Count);
        AudioClip clip = _tileDataDictionary[tile].audioClips[index]; // access current tiles  audio clip and returns it
        
        return clip; // returns the clip
    }
}

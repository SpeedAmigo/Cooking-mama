using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data", order = 1)]
public class TileData : ScriptableObject
{
   public List<TileBase> tiles = new(); 
   public List<AudioClip> audioClips = new();
   public FloorType floorType;
}

public enum FloorType
{
    Grass,
    Dirt,
    Rock,
    Wood,
}

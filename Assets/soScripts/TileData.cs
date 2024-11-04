using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "Tile Data", order = 1)]
public class TileData : ScriptableObject
{
   //public List<TileBase> tiles = new(); 
   public List<RuleTile> ruleTiles = new();
   public List<AudioClip> audioClips = new();
   public FloorType floorType;
}



using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform filed;
    [SerializeField] private MinesweeperSize minesweeperSize;

    [SerializeField] private List<TileScript> tilesList;
    [SerializeField] private List<TileScript> mineList;

    public static event Action MsDebug;
    
    private readonly HashSet<TileScript> _visitedTiles = new();
    private Dictionary<Vector2Int, TileScript> _tileDictionary = new();
    private BoardSize _boardSize;
    
    private BoardSize GetBoardSize(MinesweeperSize size)
    {
        switch (size)
        {
            case MinesweeperSize.Small:
                return new BoardSize(9, 9, 10);
            case MinesweeperSize.Medium:
                return new BoardSize(16, 16, 40);
            case MinesweeperSize.Large:
                return new BoardSize(24, 24, 99);
            default:
                throw new ArgumentException("Invalid Minesweeper size");
        }
    }
    
    private readonly List<Vector2Int> _directions = new()
    {
        new Vector2Int(0, 1), // up
        new Vector2Int(0, -1), // down
        new Vector2Int(-1, 0), // left
        new Vector2Int(1, 0), // right
        new Vector2Int(-1, 1), // up-left
        new Vector2Int(1, 1), // up-right
        new Vector2Int(-1, -1), // down-left
        new Vector2Int(1, -1), // down-right
    };


    public void CreateBoard(int width, int height, int mineCount)
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Transform tileTransform = Instantiate(tilePrefab, filed, true);
                //Vector2Int gridPosition = new Vector2Int(col, row);
                float xIndex = col - ((width - 1) / 2f);
                float yIndex = row - ((height - 1) / 2f);
                tileTransform.localPosition = new Vector2(xIndex, yIndex);
                Vector2Int gridPosition = new Vector2Int((int)xIndex, (int)yIndex);
                
                TileScript tileScript = tileTransform.GetComponent<TileScript>();
                _tileDictionary[gridPosition] = tileScript;
                tileScript.manager = this;

                Debug.Log(gridPosition);
                //tilesList.Add(tileTransform.GetComponent<TileScript>());
            }
        }
        
        PlaceMines(mineCount);
    }
    
    private void PlaceMines(int mineCount)
    {
        List<Vector2Int> avaliblePositions = new List<Vector2Int>(_tileDictionary.Keys);
        
        for (int i = 0; i < mineCount; i++)
        {
            int randomIndex = Random.Range(0, avaliblePositions.Count);
            Vector2Int randomPosition = avaliblePositions[randomIndex];
            
            TileScript mineTile = _tileDictionary[randomPosition];
            mineTile.isMine = true;
            
            avaliblePositions.RemoveAt(randomIndex);
            
            GetNeighbours(randomPosition);
        }
    }
    
    public void GetNeighbours(Vector2Int minePosition)
    {
        foreach (Vector2Int dir in _directions)
        {
            Vector2Int neighborPos = minePosition + dir;

            if (_tileDictionary.TryGetValue(neighborPos, out TileScript neighborScript))
            {
                if (!neighborScript.isMine)
                {
                    neighborScript.mineCount++;
                }
            }
        }   
    }
    
    public void DeactivateEmpty(Vector2Int gridPosition)
    {
        if (_visitedTiles.Contains(_tileDictionary[gridPosition])) return;
        
        TileScript currentTile = _tileDictionary[gridPosition];
        _visitedTiles.Add(currentTile);
        
        currentTile._spriteRenderer.sprite = currentTile.clickedTiles[currentTile.mineCount];
        
        foreach (Vector2Int dir in _directions)
        {
            Vector2Int neighborPos = gridPosition + dir;

            if (_tileDictionary.TryGetValue(neighborPos, out TileScript neighborScript))
            {
                if (neighborScript.mineCount == 0 && !_visitedTiles.Contains(neighborScript))
                {
                    DeactivateEmpty(neighborPos);
                }
                else if (!neighborScript.isMine)
                {
                    neighborScript._spriteRenderer.sprite = neighborScript.clickedTiles[neighborScript.mineCount];
                }
            }
        }
    }
    
    private void Start()
    {
        _boardSize = GetBoardSize(minesweeperSize);
        CreateBoard(_boardSize.width, _boardSize.height, _boardSize.mines);
    }
    
    #region DebugRegion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            MsDebug?.Invoke();
        }
    }
    #endregion
}

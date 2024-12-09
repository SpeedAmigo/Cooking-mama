using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.Serialization;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform filed;
    [SerializeField] private MinesweeperSize minesweeperSize;

    [SerializeField] private List<TileScript> tilesList;
    private List<TileScript> minesList = new();

    public static event Action MsDebug;
    public bool isStarted = false;
    
    [HideInInspector] public Timer timer;
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


    private void CreateBoard(int width, int height)
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Transform tileTransform = Instantiate(tilePrefab, filed, true);
                float xIndex = col - ((width - 1) / 2f);
                float yIndex = row - ((height - 1) / 2f);
                tileTransform.localPosition = new Vector2(xIndex, yIndex);
                Vector2Int gridPosition = new Vector2Int((int)xIndex, (int)yIndex);
                
                TileScript tileScript = tileTransform.GetComponent<TileScript>();
                _tileDictionary[gridPosition] = tileScript;
                tileScript.manager = this;
                tilesList.Add(tileScript);
                
                //Debug.Log(gridPosition);
            }
        }
    }

    public void GameRestart()
    {
        foreach (TileScript tileScript in tilesList)
        {
            tileScript.ResetTile();
        }
        
        isStarted = false;
        timer.StopTimer();
        timer.ResetTimer();
        _visitedTiles.Clear();
    }

    public void GameStarter(Vector2Int gridPosition)
    {
        timer.ResetTimer();
        PlaceMines(_boardSize.mines, gridPosition);
    }
    
    private void PlaceMines(int mineCount, Vector2Int gridPosition)
    {
        List<Vector2Int> avaliblePositions = new List<Vector2Int>(_tileDictionary.Keys);
        avaliblePositions.Remove(gridPosition);
        
        for (int i = 0; i < mineCount; i++)
        {
            int randomIndex = Random.Range(0, avaliblePositions.Count);
            Vector2Int randomPosition = avaliblePositions[randomIndex];
            
            TileScript mineTile = _tileDictionary[randomPosition];
            mineTile.isMine = true;
            minesList.Add(mineTile);
            //tilesList.Remove(mineTile);
            
            avaliblePositions.RemoveAt(randomIndex);
            
            GetNeighbours(randomPosition);
            ListUpdate();
        }
    }
    
    private void GetNeighbours(Vector2Int minePosition)
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
        isStarted = true;
        timer.StartTimer();
    }
    
    public void DeactivateEmpty(Vector2Int gridPosition)
    {
        if (_visitedTiles.Contains(_tileDictionary[gridPosition])) return;
        
        TileScript currentTile = _tileDictionary[gridPosition];
        _visitedTiles.Add(currentTile);
        
        currentTile._spriteRenderer.sprite = currentTile.clickedTiles[currentTile.mineCount];
        currentTile.active = false;
        
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
                    neighborScript.active = false;
                }
            }
        }
    }

    private List<TileScript> ListUpdate()
    {
        List<TileScript> updatedTilesList = tilesList.Except(minesList).ToList();
        
        return updatedTilesList;
    }

    public void UpdateTileState()
    {
        bool stateCheck = ListUpdate().All(tileScript => tileScript.active == false);

        if (stateCheck)
        {
            Debug.Log("Game Won!");
            timer.StopTimer();
        }
    }
    private void Start()
    {
        _boardSize = GetBoardSize(minesweeperSize);
        CreateBoard(_boardSize.width, _boardSize.height);
        timer = GetComponentInChildren<Timer>();
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

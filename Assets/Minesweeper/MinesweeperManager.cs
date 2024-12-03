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
    
    private BoardSize _boardSize;
    public bool gameStarted = false;
    
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

    public void CreateBoard(int width, int height, int mineCount)
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Transform tileTransform = Instantiate(tilePrefab, filed, true);
                float xIndex = col - ((width - 1) / 2f);
                float yIndex = row - ((height - 1) / 2f);
                tileTransform.localPosition = new Vector2(xIndex, yIndex);

                TileScript tileScript = tileTransform.GetComponent<TileScript>();
                //tileScript.manager = this;
                
                tilesList.Add(tileScript);
            }
        }
        PlaceMines(tilesList);
    }

    public void GameStarter()
    {
        PlaceMines(tilesList);
        gameStarted = true;
    }

    private void PlaceMines(List<TileScript> tileList)
    {
        for (int i = 0; i < _boardSize.mines; i++)
        {
            int randomIndex = Random.Range(0, tileList.Count);
            
            TileScript randomTile = tileList[randomIndex];

            TileScript tile = randomTile.GetComponent<TileScript>();
            
            tile.isMine = true;
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

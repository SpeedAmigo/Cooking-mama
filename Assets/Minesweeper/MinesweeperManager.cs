using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform filed;
    [SerializeField] private MinesweeperSize minesweeperSize;

    [SerializeField] private List<GameObject> tilesList;
    [SerializeField] private List<GameObject> mineList;

    public static event Action MsDebug;
    
    private BoardSize boardSize;
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
                throw new System.ArgumentException("Invalid Minesweeper size");
        }
    }

    public void CreateBoard(int width, int height, int mineCount)
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = filed;
                float xIndex = col - ((width - 1) / 2f);
                float yIndex = row - ((height - 1) / 2f);
                tileTransform.localPosition = new Vector2(xIndex, yIndex);
                
                tilesList.Add(tileTransform.gameObject);
            }
        }
    }

    private void PlaceMines(List<GameObject> tilesList)
    {
        List<TileScript> minesScripts = new List<TileScript>();
        
        for (int i = 0; i < boardSize.mines; i++)
        {
            int randomIndex = Random.Range(0, tilesList.Count);
            
            GameObject randomTile = tilesList[randomIndex];

            TileScript tile = randomTile.GetComponent<TileScript>();
            
            tile.isMine = true;
            
            mineList.Add(randomTile);
            tilesList.Remove(randomTile);
            
            minesScripts.Add(tile);
        }
        
        //SetNumbers(minesScripts);
    }

    private void SetNumbers(List<TileScript> minesScripts) 
    {
        /*
        foreach (TileScript mine in mineList)
        {
            List<TileScript> neighbours = mine.GetNeighbours();
            
            //Debug.Log($"Mine at {mine.transform.position} has neighbours: {neighbours.Count}");

            foreach (TileScript neighbour in neighbours)
            {
                neighbour.mineCount++;
            }
        }
        */
    }

    private void Start()
    {
        boardSize = GetBoardSize(minesweeperSize);
        CreateBoard(boardSize.width, boardSize.height, boardSize.mines);
        
        PlaceMines(tilesList);
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

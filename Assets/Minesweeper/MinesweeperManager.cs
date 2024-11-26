using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform filed;
    
    [SerializeField] private MinesweeperSize minesweeperSize;
    
    private int width;
    private int height;
    private int mineCount;

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
        this.width = width;
        this.height = height;
        this.mineCount = mineCount;
        
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = filed;
                float xIndex = col - ((width - 1) / 2f);
                float yIndex = row - ((height - 1) / 2f);
                tileTransform.localPosition = new Vector2(xIndex, yIndex);
                
            }
        }
    }

    private void Start()
    {
        BoardSize boardSize = GetBoardSize(minesweeperSize);
        CreateBoard(boardSize.width, boardSize.height, boardSize.mines);
    }
}

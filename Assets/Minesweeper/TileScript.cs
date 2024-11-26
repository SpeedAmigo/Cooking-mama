using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private List<Sprite> clickedTiles;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;
    
    private SpriteRenderer spriteRenderer;
    
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;

    private void OnMouseOver()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickedTile();
            }

            if (Input.GetMouseButtonDown(1))
            {
                flagged = !flagged;

                if (flagged)
                {
                    spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    spriteRenderer.sprite = unclickedTile;
                }
            }
        }
    }

    private void ClickedTile()
    {
        if (active && !flagged)
        {
            active = false;
            if (isMine)
            {
                //gameOver
                spriteRenderer.sprite = mineHitTile;
            }
            else
            {
                spriteRenderer.sprite = clickedTiles[mineCount];
            }
        }
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unclickedTile;
    }
}

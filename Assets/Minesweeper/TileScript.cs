using System.Collections.Generic;
using UnityEngine;

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
    
    private bool clicked;

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

    public void GetNeighbours()
    {
        List<TileScript> neighbours = new List<TileScript>();
        List<Vector2> directions = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            Vector2.up + Vector2.left,
            Vector2.up + Vector2.right,
            Vector2.down + Vector2.left,
            Vector2.down + Vector2.right
        };

        foreach (Vector2 dir in directions)
        {
            Vector2 positionOfNeighbor = new Vector2(transform.position.x, transform.position.y) + dir;
            Collider2D hit = Physics2D.OverlapPoint(positionOfNeighbor);

            if (hit != null)
            {
                Debug.DrawRay(transform.position, dir, Color.cyan, 1000f);
                TileScript tileScript = hit.GetComponent<TileScript>();

                if (!tileScript.isMine)
                {
                    tileScript.mineCount++;
                }
                neighbours.Add(tileScript);
            }
        }
        Debug.Log(neighbours.Count);
    }
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unclickedTile;
    }

    private void Start()
    {
        if (isMine)
        {
            GetNeighbours();
        }
    }

    private void OnEnable()
    {
        MinesweeperManager.MsDebug += DebugVisible;
    }

    private void OnDisable()
    {
        MinesweeperManager.MsDebug -= DebugVisible;
    }

    public void DebugVisible()
    {
        clicked = !clicked;
        
        if (active && clicked)
        {
            active = false;
            if (isMine)
            {
                spriteRenderer.sprite = mineTile;
            }
            else
            {
                spriteRenderer.sprite = clickedTiles[mineCount];
            }
        }

        if (!active && !clicked)
        {
            spriteRenderer.sprite = unclickedTile;
            active = true;
        }
    }
}

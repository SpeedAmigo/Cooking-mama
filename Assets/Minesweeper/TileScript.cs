using System;
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
    
    private SpriteRenderer _spriteRenderer;
    private bool _clicked;
    
    private readonly HashSet<TileScript> _visitedTiles = new();

    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;

    private readonly List<Vector2> _directions = new()
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
                    _spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    _spriteRenderer.sprite = unclickedTile;
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
                Debug.Log("Game Over");
                _spriteRenderer.sprite = mineHitTile;
            }
            else
            {
                if (mineCount == 0)
                {
                    DeactivateEmpty(transform.position);
                }
                _spriteRenderer.sprite = clickedTiles[mineCount];
            }
        }
    }
    
    public void GetNeighbours()
    {
        foreach (Vector2 dir in _directions)
        {
            Vector2 positionOfNeighbor = new Vector2(transform.position.x, transform.position.y) + dir;
            Collider2D hit = Physics2D.OverlapPoint(positionOfNeighbor);
            
            if ( hit != null)
            {
                Debug.DrawRay(transform.position, dir, Color.cyan, 1000f);
                TileScript tileScript = hit.GetComponent<TileScript>();

                if (!tileScript.isMine)
                {
                    tileScript.mineCount++;
                }
            }
        }   
    }

    public void DeactivateEmpty(Vector2 position)
    {
        if (_visitedTiles.Contains(this)) return;
        _visitedTiles.Add(this);
        
        _spriteRenderer.sprite = clickedTiles[mineCount];
        
        foreach (Vector2 dir in _directions)
        {
            Vector2 positionOfNeighbor = new Vector2(transform.position.x, transform.position.y) + dir;
            Collider2D hit = Physics2D.OverlapPoint(positionOfNeighbor);

            if (hit != null)
            {
                TileScript script = hit.GetComponent<TileScript>();
                    
                if (script!= null && script.mineCount == 0)
                {
                    script.DeactivateEmpty(positionOfNeighbor);
                }
                else if (script != null) {
                    script._spriteRenderer.sprite = clickedTiles[script.mineCount];
                }
            }  
        }
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = unclickedTile;
    }

    private void Start()
    {
        // it's called from here because otherwise indicators are messed up
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
        _clicked = !_clicked;
        
        if (active && _clicked)
        {
            active = false;
            if (isMine)
            {
                _spriteRenderer.sprite = mineTile;
            }
            else
            {
                _spriteRenderer.sprite = clickedTiles[mineCount];
            }
        }

        if (!active && !_clicked)
        {
            _spriteRenderer.sprite = unclickedTile;
            active = true;
        }
    }
}

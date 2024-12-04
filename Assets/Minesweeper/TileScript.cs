using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;
    public List<Sprite> clickedTiles;
    
    public SpriteRenderer _spriteRenderer;
    public MinesweeperManager manager;
    
    private bool _clicked;
    
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
                    Vector2Int gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
                    
                    manager.DeactivateEmpty(gridPosition);
                }
                _spriteRenderer.sprite = clickedTiles[mineCount];
            }
        }
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = unclickedTile;
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

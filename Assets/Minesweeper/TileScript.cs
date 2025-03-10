using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    [SerializeField] private  Sprite unclickedTile;
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

    public void MouseLeftClick()
    {
        if (active)
        {
            if (manager.isStarted == false)
            {
                manager.GameStarter(new Vector2Int((int)transform.position.x, (int)transform.position.y));
            }

            if (manager.isStarted)
            {
                ClickedTile();
            }
        }
    }

    public void MouseRightClick()
    {
        if (active)
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
    /*
    private void OnMouseOver()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manager.isStarted == false)
                {
                    manager.GameStarter(new Vector2Int((int)transform.position.x, (int)transform.position.y));
                }

                if (manager.isStarted)
                {
                    ClickedTile();
                }
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
    */
    
    private void ClickedTile()
    {
        if (active && !flagged)
        {
            active = false;
            if (isMine)
            {
                Debug.Log("Game Over");
                _spriteRenderer.sprite = mineHitTile;
                manager.timer.StopTimer();
            }
            else
            {
                if (mineCount == 0)
                {
                    Vector2Int gridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
                    
                    manager.DeactivateEmpty(gridPosition);
                }
                _spriteRenderer.sprite = clickedTiles[mineCount];
                manager.UpdateTileState();
            }
        }
    }

    public void ResetTile()
    {
        active = true;
        isMine = false;
        mineCount = 0;
        flagged = false;
        _spriteRenderer.sprite = unclickedTile;
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

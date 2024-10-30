using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    public Tilemap tilemap;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private Dictionary<TileBase, TileData> _tileDataDictionary;
    
    [SerializeField] private float _speed;
    [SerializeField] private List<TileData> _tileDataList;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _tileDataDictionary = new Dictionary<TileBase, TileData>();
        foreach (var tileData in _tileDataList)
        {
            foreach (var tile in tileData.tiles)
            {
                _tileDataDictionary[tile] = tileData;
            }
        }
    }


    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        
        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Vertical, _movement.y);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movement.x, _movement.y).normalized * (_speed * Time.fixedDeltaTime);
        
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile && _tileDataDictionary.TryGetValue(tile, out var tileData))
        {
            Debug.Log($"Standing on tile type: {tileData.floorType}");
        }
    }
}

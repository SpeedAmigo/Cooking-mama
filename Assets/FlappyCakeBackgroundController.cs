using System;
using UnityEngine;

public class FlappyCakeBackgroundController : MonoBehaviour
{
    [Range(0,1f)] public float scrollSpeed;
    [SerializeField] private Vector3 spawnPoint;
    
    public bool isSpawned = false;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        if (!isSpawned)
        {
            transform.position = spawnPoint;
        }
    }
}

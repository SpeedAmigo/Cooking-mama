using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private int _currentScore;
    private AudioSource _audioSource;
    private FcObjectPool objectPool;
    
    [SerializeField] private List<AudioClip> _scoreSound = new();
    [SerializeField] private List<GameObject> activeObjects = new();
    private HashSet<GameObject> spawnedReplacements = new();
    
    public static event Action<int> OnScorePoint; // event to score points
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        objectPool = GetComponent<FcObjectPool>();
    }

    private void Update()
    {
        activeObjects.Union(objectPool.activeObjects);
        CheckObjectPositions();
    }
    
    public void AddScorePoint(int scorePoint)
    {
        _currentScore += scorePoint;
        OnScorePoint?.Invoke(_currentScore);
        _audioSource.PlayOneShot(_scoreSound[0]);

        // trigger milestone sound every 10 points
        if (_currentScore % 10 == 0)
        {
            _audioSource.PlayOneShot(_scoreSound[1]);
        }
    }

    private void CheckObjectPositions()
    {
        foreach (GameObject obj in activeObjects)
        {
            if (obj.transform.position.x < -6f && !spawnedReplacements.Contains(obj))
            {
                spawnedReplacements.Add(obj);
                objectPool.GetObject(obj.tag);
            }

            if (obj.transform.position.x < -25f)
            {
                spawnedReplacements.Remove(obj);
                objectPool.ReturnObject(obj.tag, obj);
            }
        }
    }
}

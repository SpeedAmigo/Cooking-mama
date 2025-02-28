using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private int _currentScore;
    private AudioSource _audioSource;
    
    [SerializeField] private List<AudioClip> _scoreSound = new();

    public static event Action<int> OnScorePoint; // event to score points
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
}

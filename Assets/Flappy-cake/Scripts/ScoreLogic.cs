using TMPro;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    private void PointScored(int value)
    {
        scoreText.text = $"Score: {value}";
    }

    private void Start()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
    }
    
    private void OnEnable()
    {
        GameManagerScript.OnScorePoint += PointScored;
    }

    private void OnDisable()
    {
        GameManagerScript.OnScorePoint -= PointScored;
    }
}

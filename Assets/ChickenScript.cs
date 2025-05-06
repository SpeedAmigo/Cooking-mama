using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D chickenField;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeBetweenMoves = 5f;
    
    private Vector2 _minBoundPosition;
    private Vector2 _maxBoundPosition;

    private bool _isMoving;
    
    private Rigidbody2D _rb;
    private Animator _animator;

    [Button]
    private void MoveToPosition(Rigidbody2D body)
    {
        Vector2 targetPosition = GetRandomPosition(chickenField);
        
        float distance = Vector2.Distance(body.position, targetPosition);
        float duration = distance / speed;

        body.DOMove(targetPosition, duration).SetEase(Ease.Linear);
    }
    
    private Vector2 GetRandomPosition(BoxCollider2D field)
    {
        _minBoundPosition = field.bounds.min;
        _maxBoundPosition = field.bounds.max;
        
        Vector2 randomPosition = new Vector2(Random.Range(_minBoundPosition.x, _maxBoundPosition.x), Random.Range(_minBoundPosition.y, _maxBoundPosition.y));
        return randomPosition;
    }

    private void WalkAnimation()
    {
        /*
        Debug.Log(horizontal);
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Speed", horizontal);
        */
    }

    private IEnumerator MoveEveryXSeconds(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            MoveToPosition(_rb);
        }
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(MoveEveryXSeconds(timeBetweenMoves));
    }
    
    private void Update()
    {
        WalkAnimation();
    }
}

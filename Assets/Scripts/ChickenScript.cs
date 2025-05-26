using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D chickenField;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeBetweenMoves = 5f;
    
    private Vector2 _minBoundPosition;
    private Vector2 _maxBoundPosition;
    
    private bool _isMoving;
    private float _horizontal;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private void MoveToPosition(Rigidbody2D body)
    {
        _isMoving = true;
        Vector2 targetPosition = GetRandomPosition(chickenField);
        
        float distance = Vector2.Distance(body.position, targetPosition);
        float duration = distance / speed;
        
        SetHorizontal(body.position.x, targetPosition.x);

        body.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => OnComplete());
    }

    private void SetHorizontal(float positionX, float targetX)
    {
        if (positionX < targetX)
        {
            _horizontal = 1;
        }
        else
        {
            _horizontal = -1;
        }
    }

    private void OnComplete()
    {
        _isMoving = false;
        StartCoroutine(MoveEveryXSeconds(timeBetweenMoves));
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
        _animator.SetBool("IsMoving", _isMoving);
        _animator.SetFloat("Horizontal", _horizontal);
    }

    private IEnumerator MoveEveryXSeconds(float interval)
    {
        yield return new WaitForSeconds(interval);
        MoveToPosition(_rb);
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(MoveEveryXSeconds(1));
    }
    
    private void Update()
    {
        WalkAnimation();
    }
}

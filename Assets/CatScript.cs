using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class CatScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float timeBetweenMoves = 5f;
    [SerializeField] private NavMeshSurface surface;

    public Transform debugPoint;
    
    private float _horizontal;
    private float _vertical;
    
    [SerializeField] private bool _isMoving;
    private bool _isClicked;
    
    private Animator _animator;
    private NavMeshAgent _agent;

    private void MoveToPosition()
    {
        if (_isMoving) return;
        
        Debug.Log("Moving");
        _isMoving = true;
        Vector2 targetPosition = GetRandomPosition(surface);
        
        _agent.SetDestination(targetPosition);
        debugPoint.position = targetPosition;
        
        StartCoroutine(WaitForArrival());
    }

    private IEnumerator WaitForArrival()
    {
        while (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance)
        {
            yield return null;
        }
        Debug.Log("Reached");
        _isMoving = false;
        yield return new WaitForSeconds(timeBetweenMoves);
        MoveToPosition();
    }

    private Vector2 GetRandomPosition(NavMeshSurface navMeshSurface)
    {
        Vector2 center = navMeshSurface.transform.position;
        for (int i = 0; i < 20; i++)
        {
            Vector2 randomPosition = center + Random.insideUnitCircle * range;
            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, range, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        
        return Vector2.zero;
    }

    private IEnumerator MoveEveryXSeconds(float interval)
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(interval);
        MoveToPosition();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        StartCoroutine(MoveEveryXSeconds(1f));
    }
}

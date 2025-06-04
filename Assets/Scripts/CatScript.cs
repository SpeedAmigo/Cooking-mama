using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using Random = UnityEngine.Random;

public class CatScript : Interaction
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float timeBetweenMoves = 5f;
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private SpriteLibrary spriteLibrary;

    [SerializeField] private GameObject xDebug;
    
    private float _horizontal;
    private float _vertical;
    
    private bool _isMoving;
    private bool _isClicked;
    
    private Animator _animator;
    private NavMeshAgent _agent;

    private void MoveToPosition()
    {
        if (_isMoving) return;
        
        Vector2 targetPosition = GetRandomPosition(surface);
        xDebug.transform.position = targetPosition;
        
        _agent.SetDestination(targetPosition);
        SetValues(transform.position, targetPosition);
        
        StartCoroutine(WaitForArrival());
    }

    private IEnumerator WaitForArrival()
    {
        _isMoving = true;

        while (_agent.pathPending)
        {
            yield return null;
        }
        
        while (_agent.remainingDistance > _agent.stoppingDistance)
        {
            yield return null;
        }
        
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

    private void SetValues(Vector2 position, Vector2 target)
    {
        if (position.x <= target.x)
        {
            _horizontal = 1;
        }
        else
        {
            _horizontal = -1;
        }

        if (position.y <= target.y)
        {
            _vertical = 1;
        }
        else
        {
            _vertical = -1;
        }
    }

    private void WalkAnimation()
    {
        _animator.SetBool("IsMoving", _isMoving);
        _animator.SetFloat("Horizontal", _horizontal);
        _animator.SetFloat("Vertical", _vertical);
    }
    
    public override void Interact()
    {
        _agent.isStopped = true;
        _isClicked = true;
        _isMoving = false;
        
        _animator.SetBool("IsClicked", _isClicked);
        _animator.SetBool("IsMoving", _isMoving);
    }

    public void ResumeCatAfterPurr()
    {
        _agent.isStopped = false;
        _isClicked = false;

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        _animator.SetBool("IsClicked", _isClicked);
        _animator.SetBool("IsMoving", _isMoving);
    }

    protected override void Awake()
    {
        base.Awake();
                
        if (ES3.KeyExists("CatSkin"))
        {
            string savedSkinName = ES3.Load<string>("CatSkin");
            
            SpriteLibraryAsset skin = Resources.Load<SpriteLibraryAsset>($"CatSkinLibraries/{savedSkinName}");

            if (skin != null)
            {
                spriteLibrary.spriteLibraryAsset = skin;
            }
            else
            {
                Debug.LogWarning($"Could not find skin '{savedSkinName}' in Resources/SkinLibraries/");
            }
        }
        else
        {
            Debug.Log("No saved skin found, assigning default.");
        }
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = speed;
        
        MoveToPosition();
    }

    private void Update()
    {
        WalkAnimation();
    }
}

using UnityEngine;
using UnityEngine.Pool;

public class PipeLogic : MonoBehaviour
{
    private Rigidbody2D _body;
    private ObjectPool<PipeLogic> _pipePool;
    
    [HideInInspector]
    public GameManagerScript gameManager;
    
    [Tooltip("Speed of the pipe")]
    [SerializeField] private float _pipeSpeed;
    
    [Tooltip("Indicates how much points to add")]
    [SerializeField] private int _pipeScoreValue;
    

    public void SetPool(ObjectPool<PipeLogic> pool)
    {
        _pipePool = pool;
    }
    
    void Start()
    {
      _body = GetComponent<Rigidbody2D>();  
    }
    
    private void FixedUpdate()
    {
        _body.linearVelocity = new Vector2((-1 * _pipeSpeed), 0);

        if (gameObject.transform.position.x <= -14f)
        {
            _pipePool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.AddScorePoint(_pipeScoreValue);
    }
}

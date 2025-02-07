using UnityEngine;

public class PlayerScript : MonoBehaviour, IInputHandler
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audio;
    
    [SerializeField] private float _speed;
    [SerializeField] private WorldMapManager _worldMapManager;
    public void Step()
    {
        /*
        if (WorldMapManager.Instance != null && _audio != null)
        {
            AudioClip currentFloorClip = WorldMapManager.Instance.GetCurretnAudioClip(transform.position);
            _audio.PlayOneShot(currentFloorClip);
        }   
        */     
        if (_worldMapManager != null && _audio != null)
        {
            AudioClip currentFloorClip = _worldMapManager.GetCurretnAudioClip(transform.position);
            _audio.PlayOneShot(currentFloorClip);
        }
    }

    private void OnEnable()
    {
        InputManager.Instance.RegisterHandler(this);
    }

    private void OnDisable()
    {
        InputManager.Instance.UnregisterHandler(this);
    }
    
    public void HandleInput()
    {
        if (GameStateManager.CurrentGameState == GameState.InGame)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        
            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Vertical, _movement.y);
            _animator.SetFloat(Speed, _movement.sqrMagnitude);
        }
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * (_speed * Time.fixedDeltaTime);
    }
}

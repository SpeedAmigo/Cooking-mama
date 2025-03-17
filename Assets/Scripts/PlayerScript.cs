using UnityEngine;

public class PlayerScript : MonoBehaviour, IInputHandler
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private AudioSource _audio;
    
    [SerializeField] private float _speed;
    
    public void Step()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        
        if (WorldMapManager.Instance != null && _audio != null)
        {
            AudioClip currentFloorClip = WorldMapManager.Instance.GetCurretnAudioClip(transform.position);
            _audio.pitch = randomPitch;
            _audio.PlayOneShot(currentFloorClip);
        }   
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
        InputManager.Instance.RegisterHandler(this);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * (_speed * Time.fixedDeltaTime);
    }
}

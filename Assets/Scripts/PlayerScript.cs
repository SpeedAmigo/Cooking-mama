using FMODUnity;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IInputHandler
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private StudioEventEmitter _playerAudio;
    
    [SerializeField] private float _speed;
    
    public void Step()
    {
        _playerAudio.Play();
        
        if (WorldMapManager.Instance != null)
        {
            FloorType type = WorldMapManager.Instance.GetFloorType(transform.position);
            switch (type)
            {
                case FloorType.Wood:
                    _playerAudio.SetParameter("FloorType", 0);
                    break;
                case FloorType.Rock:
                    _playerAudio.SetParameter("FloorType", 1);
                    break;
                case FloorType.Grass:
                    _playerAudio.SetParameter("FloorType", 2);
                    break;
            }
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
        _playerAudio = GetComponent<StudioEventEmitter>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * (_speed * Time.fixedDeltaTime);
    }
}

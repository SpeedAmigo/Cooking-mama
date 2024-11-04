using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audio;
    
    [SerializeField] private float _speed;
    public void Step()
    {
        if (WorldMapManager.Instance != null && _audio != null)
        {
            AudioClip currentFloorClip = WorldMapManager.Instance.GetCurretnAudioClip(transform.position);
            _audio.PlayOneShot(currentFloorClip);
        }
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        
        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Vertical, _movement.y);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movement.x, _movement.y).normalized * (_speed * Time.fixedDeltaTime);
    }
}

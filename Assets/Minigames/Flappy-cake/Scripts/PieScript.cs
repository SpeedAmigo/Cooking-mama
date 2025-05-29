using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class PieScript : MonoBehaviour
{
    private Rigidbody2D _body;
    private PolygonCollider2D _collider;
    private bool _isAlive = true;
    private bool _isStarted = false;
    [SerializeField] private bool godMode;
    [SerializeField] private int _flyForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] PlayableDirector _director;

    [SerializeField] private UnityEvent startGameEvent;

    private void StartGame()
    {
        _body.isKinematic = false;
        startGameEvent?.Invoke();
    }
    
    private void Fly()
    {
        _body.AddForce(Vector3.up * (_flyForce * 100));

        if (_body.linearVelocity.magnitude > _maxSpeed)
        {
            _body.linearVelocity = _body.linearVelocity.normalized * _maxSpeed;
        }
    }

    private void Die()
    {
        if (godMode) return;
        _collider.enabled = false;
        _isAlive = false;
        _director.Play();
    }

    private void PieRotation()
    {
        float pieVelocity = _body.linearVelocity.y;
        
        // Determine target angle based on upward or downward movement
        float targetAngle = Mathf.Clamp(pieVelocity * _rotationSpeed, -20f, 20f);

        // Smoothly rotate towards the target angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        gameObject.transform.rotation = Quaternion.Lerp(
            gameObject.transform.rotation,
            targetRotation, 
            Time.deltaTime * _rotationSpeed);
    }
    
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isAlive && !_isStarted)
        {
            _isStarted = true;
            StartGame();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isAlive)
        {
            Fly();
        }

        if (transform.position.y is < -6 or > 6) // this is the same as || statement
        {
            Die();
        }
        
        PieRotation();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
}

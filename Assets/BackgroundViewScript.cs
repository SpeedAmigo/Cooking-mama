using UnityEngine;

public class BackgroundViewScript : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private float currentTime;
    
    private bool isInCollider = false;
    private bool moved = false;

    private void Start()
    {
        currentTime = delayTime;
    }

    private void Update()
    {
        if (isInCollider)
        {
            moved = PlayerMoved(); 
        }
        
        if (currentTime > 0 && isInCollider && !moved)
        {
            currentTime -= Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerScript>(out var player)) return;

        isInCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerScript>(out var player)) return;
        
        isInCollider = false;
        currentTime = delayTime;
    }

    private bool PlayerMoved()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputX != 0 || inputY != 0)
        {
            currentTime = delayTime;
            return true;
        }
        return false;
    }
}

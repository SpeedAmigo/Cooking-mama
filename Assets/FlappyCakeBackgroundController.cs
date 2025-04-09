using UnityEngine;

public class FlappyCakeBackgroundController : MonoBehaviour
{
    public float scrollSpeed;
    public float callingPoint;
    public float deactivationPoint;
    
    [SerializeField] protected Vector3 spawnPoint;
    
    public bool isSpawned = false;
    private bool hasCalledSpawn;

    [HideInInspector] public FcObjectPool pool;
    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.fixedDeltaTime);
    }
    
    private void Update()
    {
        if (transform.position.x < callingPoint && !hasCalledSpawn)
        {
            CallAnotherObject();
        }

        if (transform.position.x < deactivationPoint)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        hasCalledSpawn = false;
        pool.ReturnObject(gameObject.tag, gameObject);
    }

    private void CallAnotherObject()
    { 
        hasCalledSpawn = true; 
        pool.GetObject(gameObject.tag);
    }
    
    private void OnEnable()
    {
        if (!isSpawned)
        {
            transform.position = spawnPoint;
        }
    }
}

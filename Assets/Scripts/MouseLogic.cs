using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    private PlayerScript _playerScript;
    private Vector3 _screenPosition;
    private readonly OnMouseHoverLogic _onMouseHoverLogic = new();
    private readonly OnMouseInteraction _onMouseInteraction = new();

    [SerializeField] private Texture2D pickCursor;
    [SerializeField] private Texture2D interactCursor;
    [SerializeField] private Texture2D normalCursor;
    
    [SerializeField] private float _maxDistance;
    
    private bool IsWithinRange(Vector2 mousePosition, Vector2 characterPosition)
    {
        float range = Vector2.Distance(mousePosition, characterPosition);
        
        return range <= _maxDistance;
    }

    private void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
    
    private void Update()
    {
        _screenPosition = Input.mousePosition;
        Vector2 rayPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (IsWithinRange(rayPosition, _playerScript.transform.position))
        {
            _onMouseHoverLogic.OnMouseHover(rayPosition);
            //_onMouseHoverLogic.OnMouseHover(rayPosition, normalCursor, interactCursor, pickCursor);
            
            if (Input.GetMouseButtonDown(0))
            {
                _onMouseInteraction.ObjectInteraction(rayPosition);
            }
        }
        else
        {
            //Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
    
    // creates debug line to maxDistance
    private void OnDrawGizmos()
    {
        if (_playerScript == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerScript.transform.position, _maxDistance);
    }
}

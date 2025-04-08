using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private KeyCode keyCode;

    [SceneDropdown] [SerializeField] private string sceneName;

    private bool playerInRange;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            playerInRange = false;
        }
    }

    private void InputListener()
    {
        if (!playerInRange) return;
        
        if (Input.GetKeyDown(keyCode))
        {
            SceneLoader.LoadScene(sceneName);
        }
    }

    private void Update()
    {
        InputListener();
    }
}

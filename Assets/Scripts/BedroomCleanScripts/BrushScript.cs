using System.Collections;
using UnityEngine;
public class BrushScript : BedroomCleanAbstract
{
    private Animator animator;
    private bool pickedUp;
    private Vector2 animEndPos;
    private Vector2 worldPos;

    public Camera minigameCamera;
    
    protected override void OnRaycastClick()
    {
        pickedUp = true;
        animator.enabled = false; // if animator is on the brush won't move
    }

    private void ReleaseBrush()
    {
        pickedUp = false;
        StartCoroutine(MoveBrushToStartPosition(worldPos, animEndPos, 0.5f));
    }

    private IEnumerator MoveBrushToStartPosition(Vector2 mousePos, Vector2 startPos, float duration)
    {
        float elapsedTime = 0;
        Vector2.Lerp(mousePos, startPos, duration);

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(mousePos, startPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = startPos;
        animator.enabled = true;
    }
    
    public void BrushAnimation()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.Play("Brush", 0, 0f);
    }

    public void OnAnimationEnd()
    {
        animEndPos = transform.position;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (pickedUp)
        {
            Vector3 mousePos = Input.mousePosition;
            
            mousePos.z = minigameCamera.nearClipPlane;
            worldPos = minigameCamera.ScreenToWorldPoint(mousePos) * new Vector2(1.2f, 1.2f);
            gameObject.transform.position = worldPos;
            
            manager.holdingBrush = true;
        }
        else
        {
            manager.holdingBrush = false;
        }

        if (pickedUp && Input.GetMouseButtonUp(0))
        {
            ReleaseBrush();
        }
    }
}

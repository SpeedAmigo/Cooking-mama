using System;
using UnityEngine;

public class WebScript : MonoBehaviour
{
    [HideInInspector] public BedroomMinigameManager bedroomManager;
    
    [SerializeField] private float mouseMagnitude = 20f;
    [SerializeField][Range(0, 1)] private float webRemovingValue = 0.1f;
    
    private Vector2 lastMousePos;
    private Vector2 currentMousePos;
    private Vector2 deltaMousePos;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseEnter()
    {
        lastMousePos = Input.mousePosition;
    }

    private void OnMouseOver()
    {
        if (!bedroomManager.holdingBrush) return;
        
        currentMousePos = Input.mousePosition;
        deltaMousePos = currentMousePos - lastMousePos;

        if (deltaMousePos.magnitude < mouseMagnitude) return;
        
        lastMousePos = currentMousePos;
        
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp(color.a - webRemovingValue, 0f, 1f);
        spriteRenderer.color = color;

        if (spriteRenderer.color.a == 0f)
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using UnityEngine;

public class WebScript : BedroomCleanAbstract
{
    [SerializeField] private float mouseMagnitude = 20f;
    [SerializeField][Range(0, 1)] private float webRemovingValue = 0.1f;
    
    [SerializeField] private Vector2 lastMousePos;
    [SerializeField] private Vector2 currentMousePos;
    [SerializeField] private Vector2 deltaMousePos;
    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    protected override void OnRaycastClick(){}

    public override void OnRaycastEnter()
    {
        lastMousePos = Input.mousePosition;
    }
    
    public override void OnRaycastOver()
    {
        if (!manager.holdingBrush) return;
        
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

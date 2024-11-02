using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    [SerializeField] private Vector3 _screenPosition;

    private void Update()
    {
        _screenPosition = Input.mousePosition;
        Vector2 rayPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero, 15);
            
            if (hit)
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}

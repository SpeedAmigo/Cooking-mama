using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KitchenCameraMovementScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Transform> movePoints = new();
    
    private Tween tween;
    
    private void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis != 0)
        {
            MoveToPoint(xAxis);
        }
    }

    private void MoveToPoint(float xValue)
    {
        if (tween != null && tween.IsPlaying()) return;
        
        Transform nearestPoint = GetNearestPoint(mainCamera.transform, xValue);
        if (nearestPoint != null)
        {
            tween = mainCamera.transform.DOMoveX(nearestPoint.position.x, 0.5f).OnComplete(() => tween = null);
        }
    }

    private Transform GetNearestPoint(Transform cameraPosition, float direction)
    {
        Transform closest = null;
        float shortestDistance = float.MaxValue;
        float cameraX = cameraPosition.position.x;

        foreach (Transform point in movePoints)
        {
            float pointX = point.position.x;

            if (direction > 0 && pointX > cameraX)
            {
                float distance = Mathf.Abs(pointX - cameraX);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closest = point;
                }
            }
            
            else if (direction < 0 && pointX < cameraX)
            {
                float distance = Mathf.Abs(pointX - cameraX);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closest = point;
                }
            }
        }

        return closest;
    }
}

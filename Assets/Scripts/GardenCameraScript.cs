using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GardenCameraScript : MonoBehaviour
{
    [SerializeField] private Camera gardenCam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform southCameraStopper;
    
    public readonly float startOffset = 0.35f;
    public float currentOffset = 0;
    
    private Tween offsetTween;

    private Vector3 MoveCamera(Camera cam, Transform targetTransform)
    {
        float targetY = targetTransform.position.y + currentOffset;
        return new Vector3(cam.transform.position.x, targetY, cam.transform.position.z);
    }
    
    public void ChangeCameraOffset(float offset, float duration)
    {
        offsetTween?.Kill();
        offsetTween = DOTween.To(() => currentOffset, x => currentOffset = x, offset, duration);
    }
    
    private void Start()
    {
        currentOffset = startOffset;
        gardenCam = GetComponentInChildren<Camera>();
    }
    private void LateUpdate()
    {
        gardenCam.transform.position = MoveCamera(gardenCam, player);

        if (gardenCam.transform.position.y < southCameraStopper.position.y)
        {
            gardenCam.transform.position = new Vector3(gardenCam.transform.position.x, southCameraStopper.position.y, gardenCam.transform.position.z);
        }
    }
}

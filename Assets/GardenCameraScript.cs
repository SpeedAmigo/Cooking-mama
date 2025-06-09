using UnityEngine;

public class GardenCameraScript : MonoBehaviour
{
    [SerializeField] private Camera gardenCam;
    [SerializeField] private Transform player;
    
    [SerializeField] private float offset = 0;

    private Vector3 MoveCamera(Camera cam, Transform targetTransform)
    {
        float targetY = targetTransform.position.y + offset;
        return new Vector3(cam.transform.position.x, targetY, cam.transform.position.z);
    }
    
    private void Start()
    {
        gardenCam = GetComponentInChildren<Camera>();
    }
    private void LateUpdate()
    {
        gardenCam.transform.position = MoveCamera(gardenCam, player);
    }
}

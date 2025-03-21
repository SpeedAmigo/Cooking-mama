using UnityEngine;

public class TrashScript : BedroomCleanAbstract
{
    [SerializeField] private Transform binTransform;
    [SerializeField] private float speed;
    
    private bool isMoving;
    
    protected override void OnMouseDown()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(
                transform.position, 
                binTransform.position, 
                speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, binTransform.position) < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

using System;
using UnityEngine;

public class RoomTextTrigger : MonoBehaviour
{
    [SerializeField] private SoObjectText objectText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered the trigger" + other.name);
        
        if (!other.TryGetComponent<PlayerScript>(out var playerScript)) return;
        
        if (objectText != null)
        {
            objectText.ShowText(objectText.popUpText);
        }
    }
}

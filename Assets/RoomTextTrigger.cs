using System;
using DG.Tweening;
using UnityEngine;

public class RoomTextTrigger : MonoBehaviour
{
    [SerializeField] private SoObjectText objectText;
    [SerializeField][Range (1,7)] private int roomUnlockDay;
    [SerializeField] private DayNightScript dayNightScript;

    [SerializeField] private SpriteRenderer roomCover;
    private Collider2D collider;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerScript>(out var playerScript)) return;
        
        if (objectText != null)
        {
            objectText.ShowText(objectText.popUpText);
        }
    }

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        
        if (roomUnlockDay > dayNightScript.GetDayCount()) return;
        
        collider.isTrigger = true;
        if (roomCover != null)
        {
            Color color = roomCover.color;
            color.a = 0;
            
            roomCover.DOColor(color, 1.5f).OnComplete(() => roomCover.gameObject.SetActive(false));
        }
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BedroomBedScript : MinigameManager
{
    [SerializeField] private Image image;
    [SerializeField] private DayNightScript globalTimer;
    
    public override void Interact()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        if (globalTimer.CurrentDayCycle == DayCycles.Night)
        {
            ImageAnimation();
            
            globalTimer.SetDayCycle(DayCycles.Sunrise);
            
            if (globalTimer.GetDayCount() < 7)
            {
                globalTimer.IncreaseDayCount();
            }

            Debug.Log("Day Count: " + globalTimer.GetDayCount());
        }
        else
        {
            base.Interact();
        }
    }

    private void ImageAnimation()
    {
        image.gameObject.SetActive(true);
        
        Color color = image.color;
        color.a = 1;

        image.DOColor(color, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => image.gameObject.SetActive(false));
    }
}

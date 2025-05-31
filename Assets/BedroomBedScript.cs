using UnityEngine;
using UnityEngine.UI;

public class BedroomBedScript : MinigameManager
{
    [SerializeField] private Image image;
    [SerializeField] private DayNightScript globalTimer;
    
    public override void Interact()
    {
        Debug.Log("Interacting with Bedroom Bed");
        
        if (globalTimer.CurrentDayCycle == DayCycles.Night)
        {
            //image.gameObject.SetActive(true);
            
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
}

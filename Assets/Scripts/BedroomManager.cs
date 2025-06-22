using UnityEngine;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] private Sprite cleanBedSprite;
    [SerializeField] private Sprite messyBedSprite;
    
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject[] paperTrashes;
    [SerializeField] private SoObjectText firstText;
    [SerializeField] private DayNightScript dayNightScript;

    public bool bedCleaned; 

    private void Awake()
    {
        EventsManager.BedroomMinigameCompleteEvent += MinigameComplete;
    }

    private void OnDisable()
    {
        EventsManager.BedroomMinigameCompleteEvent -= MinigameComplete;
    }

    private void Start()
    {
        if (ES3.KeyExists("BedSave"))
        {
            bedCleaned = ES3.Load<bool>("BedSave");
        }

        if (!bedCleaned)
        {
            SayFirstText();
        }
        
        if (!bedCleaned) return;
        
        MinigameComplete();
        
        if (1 == dayNightScript.GetDayCount() && bedCleaned)
        {
            dayNightScript.SetDayCycle(DayCycles.Night);
        }
    }

    private void SayFirstText()
    {
        firstText.ShowTextInChain(firstText.chainText, firstText.delayTime);
        //firstText.ShowText(firstText.popUpText);
    }

    private void MinigameComplete()
    {
        bedCleaned = true;
        
        bed.GetComponent<SpriteRenderer>().sprite = cleanBedSprite;
        
        foreach (GameObject paperTrash in paperTrashes)
        {
            paperTrash.gameObject.SetActive(false);
        }
        
        ES3.Save("BedSave", bedCleaned);
    }
}

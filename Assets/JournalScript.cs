using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalScript : MonoBehaviour
{
    [SerializeField] private GameObject journalBody;
    [SerializeField] private DayNightScript dayNightScript;
    [SerializeField] [Range(1,3)] private int pageNumber;
    [SerializeField] Animator animator;
    
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;

    public List<ImageType> imageTypes = new();
    
    void Start()
    {
        journalBody.SetActive(false);
    }
    
    private void Update()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame)
        {
            journalBody.SetActive(false);
        }
    }
    
    private void ToggleBookPhoto(int pageNumber)
    {
        int dayNumber = dayNightScript.GetDayCount();
        
        switch (pageNumber)
        {
            case 1:
                leftImage.sprite = PhotoHandler(leftImage, dayNumber, imageTypes[0]);
                rightImage.sprite = PhotoHandler(rightImage, dayNumber, imageTypes[1]);
                break;
            case 2:
                leftImage.sprite = PhotoHandler(leftImage, dayNumber, imageTypes[2]);
                rightImage.sprite = PhotoHandler(rightImage, dayNumber, imageTypes[3]);
                break;
            case 3:
                leftImage.sprite = PhotoHandler(leftImage, dayNumber, imageTypes[4]);
                rightImage.gameObject.SetActive(false);
                break;
        }
    }

    private Sprite PhotoHandler(Image image, int dayNumber, ImageType imageType)
    {
        image.gameObject.SetActive(true);

        if (imageType.dayNumber == dayNumber)
        {
            imageType.text.ShowTextInChain
                (imageType.text.chainText, 
                    imageType.text.durationTime, 
                    imageType.text.initialDelayTime, 
                    imageType.text.delayTime);
        }
        
        if (imageType.dayNumber <= dayNumber)
        {
            return imageType.image;
        }
        else if (imageType.dayNumber > dayNumber)
        {
            return imageType.blurredImage;
        }

        return null;
    }
    
    public void NexPageAnimation()
    {
        if (pageNumber >= 3) return;
        animator.SetTrigger("NextPage");
        pageNumber++;
        ToggleBookPhoto(pageNumber);
    }
    
    public void PreviousPageAnimation()
    {
        if (pageNumber <= 1) return;
        animator.SetTrigger("PreviousPage");
        pageNumber--;
        ToggleBookPhoto(pageNumber);
    }

    public void ToggleJournal()
    {
        journalBody.SetActive(!journalBody.activeSelf);

        if (journalBody.activeInHierarchy)
        {
            ToggleBookPhoto(1);
        }
    }
}

[Serializable]
public class ImageType
{
    public int dayNumber;
    public Sprite image;
    public Sprite blurredImage;
    public SoObjectText text;
}

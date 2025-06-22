using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BedroomBedScript : MinigameManager
{
    public List<DayBeginText> dayBeginTexts = new();
    
    [SerializeField] private Image image;
    [SerializeField] private GameObject popUpTextParent;
    
    [SerializeField] private List<TMP_Text> dayTexts = new();
    [SerializeField] private TMP_Text skipText;

    private bool     waitingForInput;
    private bool            finished;
    private Sequence        sequence;
    private Sequence    textSequence;
    
    public override void Interact()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        if (dayNightScript.CurrentDayCycle == DayCycles.Night)
        {
            ImageAnimation();
        }
        else
        {
            base.Interact();
        }
    }

    // fade in animation for black overlay image
    private void ImageAnimation()
    {
        GameStateManager.ChangeGameState(GameState.DayTransition);
        image.gameObject.SetActive(true);
        
        ResetTextsAlpha();
        
        sequence = DOTween.Sequence();
        
        sequence.Append(image.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .OnStepComplete(() =>
            { 
                StartTextSequence();
                ChangeDayCycle();
                waitingForInput = true;
                sequence.Pause();
            }))
            .Append(image.DOFade(0f, 1f)
                .OnStart(() => SequenceFinished()));
    }

    private void SequenceFinished()
    {
        GameStateManager.ChangeGameState(GameState.InGame);
        popUpTextParent.SetActive(false);
    }
    
    // general method for triggering texts 
    private void StartTextSequence()
    {
        finished = false;
        
        textSequence = DOTween.Sequence();

        var targetText = GetCurrentDayText(dayBeginTexts);
        
        popUpTextParent.SetActive(true);
        
        FadeInDayTexts(targetText);
        
        textSequence.OnComplete(() => TextSequenceComplete());
    }
    
    // after textSequence completed the skip button appears and finished flag is triggered
    private void TextSequenceComplete()
    {
        finished = true;
        skipText.gameObject.SetActive(true);
        skipText.DOFade(1f, 1f);
    }

    // this animates the list of texts from 0 to 1 except from skipText
    private void FadeInDayTexts(DayBeginText targetText)
    {
        for (int i = 0; i < dayTexts.Count; i++)
        {
            var textElement  = dayTexts[i];
            textElement.gameObject.SetActive(true);
            textElement.text = targetText.textList[i];
            
            textSequence.Append(dayTexts[i].DOFade(1f, 1f));
            textSequence.AppendInterval(1f);
        }
    }

    // to display correct messages for each day,
    // you need this method to find witch text list to choose
    private DayBeginText GetCurrentDayText(List<DayBeginText> dayBeginTexts)
    {
        DayBeginText targetDayText = new();
        
        foreach (var dayBeginText in dayBeginTexts)
        {
            if (dayBeginText.textForDay == dayNightScript.GetDayCount())
            { 
                targetDayText = dayBeginText;
            }
        }

        return targetDayText;
    }

    private void ResetTextsAlpha()
    {
        foreach (var text in dayTexts)
        {
            var color = text.color;
            color.a = 0f;
            text.color = color;
            text.gameObject.SetActive(false);
        }
        
        var skipColor = skipText.color;
        skipColor.a = 0f;
        skipText.color = skipColor;
        skipText.gameObject.SetActive(false);
    }
    
    private void ChangeDayCycle()
    {
        dayNightScript.SetDayCycle(DayCycles.Sunrise);
            
        if (dayNightScript.GetDayCount() < 7)
        {
            dayNightScript.IncreaseDayCount();
        }
    }

    private void Update()
    {
        if (waitingForInput && finished && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForInput = false;
            sequence.Play();
        }
    }
}

[Serializable]
public class DayBeginText
{
    public List<string> textList;
    public int textForDay;
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BedroomBedScript : MinigameManager
{
    [SerializeField] private Image image;
    [SerializeField] private DayNightScript globalTimer;

    private bool waitingForInput;
    private Sequence sequence;
    private Sequence textSequence;
    
    public List<DayBeginText> dayBeginTexts = new();
    
    public override void Interact()
    {
        if (GameStateManager.CurrentGameState != GameState.InGame) return;
        
        if (globalTimer.CurrentDayCycle == DayCycles.Night)
        {
            ImageAnimation();
        }
        else
        {
            base.Interact();
        }
    }

    private void ImageAnimation()
    {
        image.gameObject.SetActive(true);
        
        sequence = DOTween.Sequence();
        
        sequence.Append(image.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .OnStepComplete(() =>
            { 
                ChangeDayCycle();
                waitingForInput = true;
                sequence.Pause();
            }))
            .Append(image.DOFade(0f, 1f));
    }

    private void StartTextSequence()
    {
        textSequence = DOTween.Sequence();

        DayBeginText targetText = new();

        foreach (var dayBeginText in dayBeginTexts)
        {
            if (dayBeginText.textForDay == globalTimer.GetDayCount())
            {
                targetText = dayBeginText;
            }
        }

        foreach (var text in targetText.textList)
        {
            text.gameObject.SetActive(true);

            textSequence.Append(text.DOFade(1f, 1f));
            textSequence.AppendInterval(1f);
        }
    }
    
    private void ChangeDayCycle()
    {
        globalTimer.SetDayCycle(DayCycles.Sunrise);
            
        if (globalTimer.GetDayCount() < 7)
        {
            globalTimer.IncreaseDayCount();
        }

        Debug.Log("Day Count: " + globalTimer.GetDayCount());
    }

    private void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForInput = false;
            sequence.Play();
        }
    }
}

[Serializable]
public class DayBeginText
{
    public List<TMP_Text> textList;
    public TMP_Text skipText;
    public int textForDay;
}

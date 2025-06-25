using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class StartPopUpScript : MonoBehaviour, IInputHandler
{
    [SerializeField] private GameObject narratorPopUp;
    
    [SerializeField] private UnityEvent onDisable;
    [SerializeField] private List<TMPro.TMP_Text> texts;
    [SerializeField] private TMPro.TMP_Text skipText;

    [SerializeField] private bool finished;
    [SerializeField] private bool popUpShowed;
    
    private void Start()
    {
        if (ES3.KeyExists("PopUpShowed"))
        {
            popUpShowed = ES3.Load<bool>("PopUpShowed");
        }

        if (popUpShowed)
        {
            onDisable?.Invoke();
            return;
        }
        
        narratorPopUp.gameObject.SetActive(true);
        InputManager.Instance.RegisterHandler(this);
        GameStateManager.ChangeGameState(GameState.PauseMenu);
        
        SetTextsAlpha(0);
        ShowTexts();
    }

    private void ShowTexts()
    {
        Sequence sequence = DOTween.Sequence();
        
        foreach (var text in texts)
        {
            text.gameObject.SetActive(true);
            
            Color targetColor = text.color;
            targetColor.a = 1;
            
            sequence.Append(text.DOColor(targetColor, 1.5f));
            
            sequence.AppendInterval(2f);
        }
        
        sequence.OnComplete(() => FinishedTextSequence());
    }

    private void FinishedTextSequence()
    {
        finished = true;
        
        skipText.gameObject.SetActive(true);
            
        Color targetColor = skipText.color;
        targetColor.a = 1;
        
        skipText.DOColor(targetColor, 1.5f).SetDelay(2f);
    }

    private void SetTextsAlpha(float alpha)
    {
        foreach (var text in texts)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            InputManager.Instance.UnregisterHandler(this);
            GameStateManager.ChangeGameState(GameState.InGame);
            onDisable?.Invoke();
            popUpShowed = true;
            narratorPopUp.gameObject.SetActive(false);
            ES3.Save("PopUpShowed", popUpShowed);
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && finished)
        { 
            InputManager.Instance.UnregisterHandler(this);
            GameStateManager.ChangeGameState(GameState.InGame);
            onDisable?.Invoke();
            popUpShowed = true;
            narratorPopUp.gameObject.SetActive(false);
            ES3.Save("PopUpShowed", popUpShowed);
        }
    }
}

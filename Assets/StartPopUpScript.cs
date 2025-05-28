using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class StartPopUpScript : MonoBehaviour, IInputHandler
{
    [SerializeField] private GameObject NarratorPopUp;
    
    [SerializeField] private UnityEvent onDisable;
    [SerializeField] private List<TMPro.TMP_Text> texts;

    [SerializeField] private bool finished;
    [SerializeField] private bool popUpShowed;
    
    private void Start()
    {
        if (ES3.KeyExists("PopUpShowed"))
        {
            popUpShowed = ES3.Load<bool>("PopUpShowed");
        }
        
        if (popUpShowed) return;
        
        NarratorPopUp.gameObject.SetActive(true);
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
        
        sequence.OnComplete(() => finished = true);
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

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && finished)
        { 
            InputManager.Instance.UnregisterHandler(this);
            GameStateManager.ChangeGameState(GameState.InGame);
            onDisable?.Invoke();
            popUpShowed = true;
            ES3.Save("PopUpShowed", popUpShowed);
            NarratorPopUp.gameObject.SetActive(false);
        }
    }
}

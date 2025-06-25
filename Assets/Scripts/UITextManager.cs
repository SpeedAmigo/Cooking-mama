using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    [SerializeField] private bool showingChainText;
    private string[] currentChainTexts; 
    private int currentChainTextIndex;
    
    
    [SerializeField] private Image textHolder;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float showupSpeed = 0.5f;
    
    [SerializeField] [Unit(Units.Second)] private float timeCounter;
    [SerializeField] [Unit(Units.Second)] private float initialTimeCounter;
    
    private void OnEnable()
    {
        EventsManager.ShowObjectText += ShowText;
        EventsManager.HideObjectText += HideObjectText;
        EventsManager.ClearObjectText += ClearObjectText;
        EventsManager.ShowChainText += ShowChainText;
    }

    private void OnDisable()
    {
        EventsManager.ShowObjectText -= ShowText;
        EventsManager.HideObjectText -= HideObjectText;
        EventsManager.ClearObjectText -= ClearObjectText;
        EventsManager.ShowChainText -= ShowChainText;
    }

    private void Start()
    {
        textHolder.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!showingChainText && timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;

            if (timeCounter <= 0)
            {
                timeCounter = 0;
                StartCoroutine(HideText());
            }
        }

        if (showingChainText && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextTextInChain();
        }
        
        if (!showingChainText && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(HideText());
            timeCounter = 0f;
        }
    }
    
    private void ShowText(string passedText)
    {
        timeCounter = initialTimeCounter;
        StartCoroutine(ShowTextCorutine(passedText));
    }
    
    private void ShowChainText(string[] passedTexts)
    {
        showingChainText = true;
        currentChainTexts = passedTexts;
        currentChainTextIndex = 0;
        
        ShowNextTextInChain();
    }
    private void ShowNextTextInChain()
    {
        if (currentChainTextIndex >= currentChainTexts.Length)
        {
            showingChainText = false;
            StartCoroutine(HideText());
            return;
        }

        string nextText = currentChainTexts[currentChainTextIndex];
        currentChainTextIndex++;

        StopAllCoroutines();
        StartCoroutine(ShowTextCorutine(nextText));
    }

    private void HideObjectText()
    {
        StartCoroutine(HideText());
    }

    private void ClearObjectText()
    {
        StopAllCoroutines();
        
        text.text = "";
        textHolder.material.SetFloat("_Progress", 0f);
        textHolder.gameObject.SetActive(false);
        
        showingChainText = false;
        currentChainTexts = null;
        currentChainTextIndex = 0;
        timeCounter = 0f;
    }
    
    private IEnumerator ShowTextCorutine(string passedText)
    {
        textHolder.gameObject.SetActive(true);
        textHolder.material.SetFloat("_Progress", 0);
        text.text = "";
        
        float elapsedTime = 0f;

        while (elapsedTime < showupSpeed)
        {
            elapsedTime += Time.deltaTime;
            textHolder.material.SetFloat("_Progress", 1 * elapsedTime / showupSpeed);
            yield return null;
        }
        
        text.text = passedText;
    }

    private IEnumerator HideText()
    {
        textHolder.material.SetFloat("_Progress", 1);
        text.text = "";
        
        float elapsedTime = 0f;

        while (elapsedTime < showupSpeed)
        {
            elapsedTime += Time.deltaTime;
            textHolder.material.SetFloat("_Progress", 1 - elapsedTime / showupSpeed);
            yield return null;
        }
        
        textHolder.gameObject.SetActive(false);
    }
}

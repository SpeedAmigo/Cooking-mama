using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    [SerializeField] private Image textHolder;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float showupSpeed = 0.5f;
    
    [SerializeField] [Unit(Units.Second)] private float timeCounter;
    
    private void OnEnable()
    {
        EventsManager.ShowObjectText += ShowText;
    }

    private void OnDisable()
    {
        EventsManager.ShowObjectText -= ShowText;
    }

    private void Start()
    {
        textHolder.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;

            if (timeCounter <= 0)
            {
                timeCounter = 0;
                StartCoroutine(HideText());
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(HideText());
            timeCounter = 0f;
        }
    }
    
    private void ShowText(string passedText)
    {
        timeCounter = 5f;
        StartCoroutine(ShowTextCorutine(passedText));
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

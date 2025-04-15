using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour
{
    
    [SerializeField] private Image textHolder;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float showupSpeed = 0.5f;
    
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(HideText());
        }
    }

    
    private void ShowText(string passedText)
    {
        /*
        textHolder.gameObject.SetActive(true);
        text.text = passedText;

        float elapsedTime = 0;
        */
        
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

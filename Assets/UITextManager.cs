using TMPro;
using UnityEngine;

public class UITextManager : MonoBehaviour
{
    [SerializeField] private GameObject textHolder;
    [SerializeField] private TMP_Text text;
    
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
        textHolder.SetActive(false);
    }

    private void ShowText(string passedText)
    {
        textHolder.gameObject.SetActive(true);
        text.text = passedText;
    }
}

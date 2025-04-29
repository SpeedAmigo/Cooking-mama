using UnityEngine;

public class BedroomStairsLogicManager : MonoBehaviour
{
    [SerializeField] private BedroomManager bedroomManager;
    [SerializeField] private StairsController stairsController;
    //[SerializeField] private string[] popUpText;
    [SerializeField] private SoObjectText soObjectText;

    private void Start()
    {
        bedroomManager = GetComponentInParent<BedroomManager>();
    }

    private void ShowText(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        EventsManager.InvokeShowObjectText(text);
    }

    private string GetRandomText(string[] texts)
    {
        if (texts.Length <= 0) return null;
        
        string text = texts[Random.Range(0, texts.Length)];
        return text;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerScript>(out PlayerScript playerScript))
        {
            if (bedroomManager.bedCleaned)
            {
                stairsController.CanTransition = true;
            }
            else
            {
                stairsController.CanTransition = false;
                ShowText(GetRandomText(soObjectText.popUpText));
            }
        }
    }
}

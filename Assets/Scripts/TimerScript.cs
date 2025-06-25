using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(float seconds)
    {
        text.text = seconds.ToString();
    }
}

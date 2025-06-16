using System.Collections.Generic;
using UnityEngine;

public class KitchenBookUI : MonoBehaviour
{
    [SerializeField] private Animator bookAnimator;
    [SerializeField] [Range(1, 5)] private int pageNumber;
    [SerializeField] DayNightScript dayNightScript;

    [SerializeField] private List<GameObject> recipes;

    private void Start()
    {
        ToggleBookText(1);
    }

    private void ToggleBookText(int pageNumber)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (i == pageNumber - 1)
            {
                recipes[i].SetActive(true);
            }
            else
            {
                recipes[i].SetActive(false);
            }
        }
    }
    
    public void NexPageAnimation()
    {
        if (pageNumber >= 5 || pageNumber == dayNightScript.GetDayCount()) return;
        bookAnimator.SetTrigger("NextPage");
        pageNumber++;
        ToggleBookText(pageNumber);
    }

    public void PreviousPageAnimation()
    {
        if (pageNumber <= 1) return;
        bookAnimator.SetTrigger("PreviousPage");
        pageNumber--;
        ToggleBookText(pageNumber);
    }
}

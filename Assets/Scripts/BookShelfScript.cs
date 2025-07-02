using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BookShelfScript : MonoBehaviour
{
    [SerializeField] private List<ShelfManager> shelfs = new();
    [SerializeField] private Camera cam;
    [SerializeField] private SoObjectText endText;

    private void Awake()
    {
        foreach (ShelfManager shelf in shelfs)
        {
            shelf.minigameCamera = cam;
        }
    }
    
    public void CheckShelfs()
    {
        for (int i = 0; i < shelfs.Count; i++)
        {
            if (!shelfs[i].completed)
            {
                return;
            }
        }
        endText.ShowChainText(endText.chainText);
    }
}

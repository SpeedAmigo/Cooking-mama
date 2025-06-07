using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BookShelfScript : MonoBehaviour
{
    [SerializeField] private List<ShelfManager> shelfs = new();

    public void CheckShelfs()
    {
        for (int i = 0; i < shelfs.Count; i++)
        {
            if (!shelfs[i].completed)
            {
                return;
            }
        }
        Debug.Log("CorrectOrder!");
    }
}

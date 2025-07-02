using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class BookShelfScript : MonoBehaviour
{
    [SerializeField] private List<ShelfManager> shelfs = new();
    [SerializeField] private Camera cam;
    [SerializeField] private SoObjectText endText;

    [SerializeField] private EventReference correctSoundRef;
    private FMOD.Studio.EventInstance correctSoundInstance;

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
        
        correctSoundInstance = RuntimeManager.CreateInstance(correctSoundRef);
        Debug.Log(correctSoundInstance);
        correctSoundInstance.start();
        correctSoundInstance.release();
    }
}

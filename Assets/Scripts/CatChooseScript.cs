using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class CatChooseScript : MonoBehaviour
{
    [SerializeField] private List<SpriteLibraryAsset> skinLibrary;
    [SerializeField] private SpriteLibrary currentLibrary;

    private int currentSkinIndex;
    
    private void Start()
    {
        currentLibrary.spriteLibraryAsset = skinLibrary[0];
    }

    public void ChangeToNextSkin()
    {
        if (skinLibrary.Count == 0 || currentLibrary == null) return;
        
        currentSkinIndex = (currentSkinIndex + 1) % skinLibrary.Count;
        currentLibrary.spriteLibraryAsset = skinLibrary[currentSkinIndex];
    }
    
    public void ChangeToPreviousSkin()
    {
        if (skinLibrary.Count == 0 || currentLibrary == null) return;
        
        currentSkinIndex = (currentSkinIndex - 1) % skinLibrary.Count;
        if (currentSkinIndex < 0) currentSkinIndex = skinLibrary.Count - 1;
        
        currentLibrary.spriteLibraryAsset = skinLibrary[currentSkinIndex];
    }

    public void SaveSelectedSkin()
    {
        ES3.Save("CatSkin", currentLibrary.spriteLibraryAsset.name);
    }
}

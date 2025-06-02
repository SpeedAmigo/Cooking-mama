using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Sirenix.OdinInspector;

public class PreferenceLoaderScript : MonoBehaviour
{
    public List<VcaType> vcaTypes = new();
    
    private void Awake()
    {
        LoadPreferences();
    }
    
    public void LoadPreferences()
    {
        LoadVolumePreferences();
        LoadResolutionPreferences();
    }

    private void LoadVolumePreferences()
    {
        foreach (var vcaType in vcaTypes)
        {
            string vcaPath = $"vca:/{vcaType.type.ToString()}";
            var vca = RuntimeManager.GetVCA(vcaPath);
            
            float volume = PlayerPrefs.GetFloat(vcaPath, vcaType.defaultVolume);
            
            vca.setVolume(volume);
        }
    }

    private void LoadResolutionPreferences()
    {
        int width = PlayerPrefs.GetInt("screenWidth", Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt("screenHeight", Screen.currentResolution.height);
        bool fullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1 ? true : false;
        
        Screen.SetResolution(width, height, fullScreen);
    }
}

[Serializable]
public class VcaType
{
    public VCAType type;
    public float defaultVolume;
}

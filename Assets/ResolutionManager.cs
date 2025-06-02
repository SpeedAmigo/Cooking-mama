using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    
    private Resolution[] resolutions;

    private void Awake()
    {
        resolutions = Screen.resolutions;
    }
    
    private void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new();
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            
            //int screenWidth = Screen.currentResolution.width;
            //int screenHeight = Screen.currentResolution.height;
            
            int savedWidth = PlayerPrefs.GetInt("screenWidth", Screen.currentResolution.width);
            int savedHeight = PlayerPrefs.GetInt("screenHeight", Screen.currentResolution.height);

            if (resolutions[i].width == savedWidth && resolutions[i].height == savedHeight)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        fullScreenToggle.isOn = PlayerPrefs.GetInt("fullScreen", 1) == 1 ? true : false;
    }

    public void DropdownValueChanged()
    {
        SetResolution(resolutionDropdown.value);
    }

    public void ToggleValueChange()
    {
        SetFullScreen(fullScreenToggle.isOn);
    }

    private void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        
        SaveFullScreen(isFullScreen);
    }
    
    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
        SaveResolution(resolution.width, resolution.height);
    }

    private void SaveResolution(int width, int height)
    {
        PlayerPrefs.SetInt("screenWidth", width);
        PlayerPrefs.SetInt("screenHeight", height);
        PlayerPrefs.Save();
    }

    private void SaveFullScreen(bool fullScreen)
    {
        PlayerPrefs.SetInt("fullScreen", fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    } 
    
    private void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(_ => DropdownValueChanged());
        fullScreenToggle.onValueChanged.AddListener(_ => ToggleValueChange());
    }

    private void OnDisable()
    {
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        fullScreenToggle.onValueChanged.RemoveAllListeners();
    }
}

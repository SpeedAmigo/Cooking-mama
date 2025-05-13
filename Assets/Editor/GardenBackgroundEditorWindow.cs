using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class GardenBackgroundEditorWindow : OdinEditorWindow
{
    [TabGroup("sunrise")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<Material> sunriseMaterials;
    [TabGroup("day")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<Material> dayMaterials;
    [TabGroup("sunset")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<Material> sunsetMaterials;
    [TabGroup("night")]
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<Material> nightMaterials;
    
    [MenuItem("Tools/Garden Background Editor")]
    private static void OpenWindow()
    {
        GetWindow<GardenBackgroundEditorWindow>().Show();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LoadAllMaterials();
    }

    private void LoadAllMaterials()
    {
        LoadSunriseMaterials();
        LoadDayMaterials();
        LoadSunsetMaterials();
        LoadNightMaterials();
    }

    private void LoadSunriseMaterials()
    {
        sunriseMaterials = new List<Material>();
        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Materials/BackgroundGarden/SunriseLayers" });
        
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (asset != null)
            {
                sunriseMaterials.Add(asset);
            }
        }
    }
    
    private void LoadDayMaterials()
    {
        dayMaterials = new List<Material>();
        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Materials/BackgroundGarden/DayLayers" });
        
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (asset != null)
            {
                dayMaterials.Add(asset);
            }
        }
    }
    
    private void LoadSunsetMaterials()
    {
        sunsetMaterials = new List<Material>();
        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Materials/BackgroundGarden/SunsetLayers" });
        
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (asset != null)
            {
                sunsetMaterials.Add(asset);
            }
        }
    }
    
    private void LoadNightMaterials()
    {
        nightMaterials = new List<Material>();
        string[] guids = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Materials/BackgroundGarden/NightLayers" });
        
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (asset != null)
            {
                nightMaterials.Add(asset);
            }
        }
    }
}

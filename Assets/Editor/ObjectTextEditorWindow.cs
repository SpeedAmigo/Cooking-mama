using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class ObjectTextEditorWindow : OdinEditorWindow
{
    [MenuItem("Tools/ObjectTextEditor")]
    private static void OpenWindow()
    {
        GetWindow<ObjectTextEditorWindow>().Show();
    }
    
    [ListDrawerSettings(Expanded = true, DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<SoObjectText> allObjectsMessages;

    protected override void OnEnable()
    {
        base.OnEnable();
        LoadAllMessages();
    }

    private void LoadAllMessages()
    {
        allObjectsMessages = new List<SoObjectText>();
        string[] guids = AssetDatabase.FindAssets("t:SoObjectText", new[] { "Assets/SO_ObjectsMessages" });

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<SoObjectText>(path);
            if (asset != null)
            {
                allObjectsMessages.Add(asset);
            }
        }
    }

    [Button("Create New Object Message")]
    private void CreateNewMessage()
    {
        string folderPath = "Assets/SO_ObjectsMessages";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "SO_ObjectsMessages");
        }
        
        string fileName = "New Object Message";

        string path = EditorUtility.SaveFilePanelInProject(
            "Save New Object",
            fileName,
            "asset",
            "Enter a name of the new Object message",
            folderPath
            );

        if (!string.IsNullOrEmpty(path))
        {
            var newMsg = ScriptableObject.CreateInstance<SoObjectText>();
            AssetDatabase.CreateAsset(newMsg, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            allObjectsMessages.Add(newMsg);
        }
    }

    [HorizontalGroup]
    [Button("Refresh All Messages")]
    private void RefreshAllMessages()
    {
        AssetDatabase.Refresh();
        LoadAllMessages();
    }

    [HorizontalGroup]
    [Button("Save All Messages")]
    private void SaveAllMessages()
    {
        AssetDatabase.SaveAssets();
    }
}

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
}

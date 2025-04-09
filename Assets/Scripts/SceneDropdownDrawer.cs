
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneDropdownAttribute))]
public class SceneDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path))
            .ToArray();

        int currentIndex = Mathf.Max(0, System.Array.IndexOf(scenes, property.stringValue));
        int selectedIndex = EditorGUI.Popup(position, currentIndex, scenes);

        if (selectedIndex >= 0 && selectedIndex < scenes.Length)
        {
            property.stringValue = scenes[selectedIndex];
        }
    }
}
#endif
